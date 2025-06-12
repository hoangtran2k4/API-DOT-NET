using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderItemService.GetAllAsync();
            return StatusCode(result.HttpStatusCode, result);
        }

        // GET: api/OrderItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _orderItemService.GetByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderItem/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid order item data.");

            var result = await _orderItemService.CreateAsync(orderItem);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderItem/update
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid || orderItem.OrderItemID <= 0)
                return BadRequest("Invalid order item data.");

            var result = await _orderItemService.UpdateAsync(orderItem);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderItem/delete/5
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _orderItemService.DeleteAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }
    }
}
