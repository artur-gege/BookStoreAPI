namespace BookStoreAPI.Domain.ModelsDTO
{
    public class CreateOrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
