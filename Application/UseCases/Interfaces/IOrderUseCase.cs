using BookStoreAPI.Domain.ModelsDTO;
using BookStoreAPI.Domain.Models;

namespace BookStoreAPI.Application.UseCases.Interfaces
{
    public interface IOrderUseCase
    {
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<List<OrderDTO>> GetOrdersByFilterAsync(string orderNumber, DateTime? orderDate);
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO);
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task DeleteOrderByIdAsync(int id);
    }
}
