namespace BookStoreAPI.Domain.Models
{
    public class OrderBook
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; } // Количество книг в заказе

        public Order? Order { get; set; }
        public Book? Book { get; set; }
    }
}
