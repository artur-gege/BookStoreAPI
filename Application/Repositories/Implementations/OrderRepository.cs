using BookStoreAPI.Application.Repositories.Interfaces;
using BookStoreAPI.Domain.Models;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Repositories.Implementations
{
    public class OrderRepository :  IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>?> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderBooks)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderBooks) // Добавили Include
                .ThenInclude(ob => ob.Book) // Если нужны данные о книгах
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrdersByFilterAsync(string orderNumber, DateTime? orderDate)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(orderNumber))
            {
                query = query.Where(o => o.OrderNumber.Contains(orderNumber));
            }

            if (orderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate == orderDate.Value);
            }

            return await query.Include(o => o.OrderBooks) // Добавили Include
                .ThenInclude(ob => ob.Book) // Если нужны данные о книгах
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order orderToCreate)
        {
            _context.Orders.Add(orderToCreate);
            await SaveAsync();
            return orderToCreate;
        }

        public async Task<Order> UpdateOrderAsync(Order orderToUpdate)
        {
            _context.Entry(orderToUpdate).State = EntityState.Modified;
            await SaveAsync();
            return orderToUpdate;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var orderToDelete = await _context.Orders.FindAsync(id);

            if (orderToDelete != null)
            {
                _context.Orders.Remove(orderToDelete);
                await SaveAsync();
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
