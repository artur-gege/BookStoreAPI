using AutoMapper;
using BookStoreAPI.Application.Repositories.Interfaces;
using BookStoreAPI.Application.UseCases.Interfaces;
using BookStoreAPI.Application.CustomExceptions;
using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;
using FluentValidation;

namespace BookStoreAPI.Application.UseCases.Implementations
{
    public class OrderUseCase : IOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDTO> _createOrderValidator;

        public OrderUseCase(IOrderRepository orderRepository, IBookRepository bookRepository, IMapper mapper, IValidator<CreateOrderDTO> createOrderValidator)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Заказ с ID {id} не найден.");
            }

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task DeleteOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Заказ с ID {id} не найден.");
            }
            await _orderRepository.DeleteOrderAsync(id);
        }

        public async Task<List<OrderDTO>> GetOrdersByFilterAsync(string orderNumber, DateTime? orderDate)
        {
            var orders = await _orderRepository.GetOrdersByFilterAsync(orderNumber, orderDate);
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            // Валидация DTO
            var validationResult = await _createOrderValidator.ValidateAsync(createOrderDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Проверка на наличие книг
            if (createOrderDTO.OrderItems == null || !createOrderDTO.OrderItems.Any())
            {
                throw new BadRequestException("Заказ должен содержать хотя бы один товар.");
            }

            var order = new Order { OrderDate = DateTime.Now, OrderNumber = GenerateOrderNumber() };
            order.OrderBooks = new List<OrderBook>();

            foreach (var item in createOrderDTO.OrderItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(item.BookId);

                if (book == null)
                {
                    throw new BookNotFoundException($"Книга с ID {item.BookId} не найдена.");
                }

                if (item.Quantity <= 0)
                {
                    throw new BadRequestException($"Количество товаров в заказе должно быть больше нуля. Книга с ID {item.BookId}");
                }

                order.OrderBooks.Add(new OrderBook { BookId = item.BookId, Book = book, Order = order, BookQuantity = item.Quantity });
            }

            await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }

        private string GenerateOrderNumber()
        {
            return Guid.NewGuid().ToString();
        }
    }
}