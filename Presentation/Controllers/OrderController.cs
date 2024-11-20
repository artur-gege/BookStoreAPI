using BookStoreAPI.Application.UseCases.Interfaces;
using BookStoreAPI.Domain.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderUseCase _orderUseCase;

        public OrderController(IOrderUseCase orderUseCase)
        {
            _orderUseCase = orderUseCase;
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _orderUseCase.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("ordersByFilter")]
        public async Task<IActionResult> GetOrdersByFilterAsync([FromQuery] string orderNumber, [FromQuery] DateTime? orderDate = null)
        {
            var orders = await _orderUseCase.GetOrdersByFilterAsync(orderNumber, orderDate);
            return Ok(orders);
        }

        [HttpGet]
        [Route("orders/{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await _orderUseCase.GetOrderByIdAsync(id);
            return Ok(order);
        }


        [HttpPost]
        [Route("orders")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDTO createOrderDTO)
        {
            var createdOrder = await _orderUseCase.CreateOrderAsync(createOrderDTO);
            return Ok(createdOrder);
            
            //return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpDelete]
        [Route("orders/{id}")]
        public async Task<IActionResult> DeleteOrderByIdAsync(int id)
        {
            await _orderUseCase.DeleteOrderByIdAsync(id);
            return NoContent();
        }

    }
}
