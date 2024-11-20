namespace BookStoreAPI.Domain.ModelsDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}