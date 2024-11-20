using AutoMapper;
using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;

namespace BookStoreAPI.Application.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>()
           .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderBooks.Select(ob => new OrderItemDTO { BookId = ob.BookId, Quantity = ob.BookQuantity })));

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<OrderItemDTO, OrderBook>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<OrderBook, OrderItemDTO>().ReverseMap();
        }
    }
}
