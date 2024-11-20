namespace BookStoreAPI.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }

        public List<OrderBook>? OrderBooks { get; set; } // Связь многие-ко-многим с заказами
    }
}
