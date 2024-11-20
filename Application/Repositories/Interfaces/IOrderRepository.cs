using BookStoreAPI.Domain.Models;
using System.Linq.Expressions;

namespace BookStoreAPI.Application.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order?>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<List<Order>?> GetOrdersByFilterAsync(string orderNumber, DateTime? orderDate);
        Task<Order> CreateOrderAsync(Order orderToCreate);
        Task<Order> UpdateOrderAsync(Order orderToUpdate);
        Task DeleteOrderAsync(int id);
        Task SaveAsync();
    }
}
