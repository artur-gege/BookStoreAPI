namespace BookStoreAPI.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        public List<OrderBook>? OrderBooks { get; set; } // Связь многие-ко-многим с книгами
    }
}