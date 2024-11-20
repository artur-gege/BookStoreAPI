using BookStoreAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBook>()
                .HasKey(ob => new { ob.OrderId, ob.BookId });

            modelBuilder.Entity<OrderBook>()
                .HasOne(ob => ob.Order)
                .WithMany(o => o.OrderBooks)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<OrderBook>()
                .HasOne(ob => ob.Book)
                .WithMany(b => b.OrderBooks)
                .HasForeignKey(ob => ob.BookId);

            modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
        }
    }
}