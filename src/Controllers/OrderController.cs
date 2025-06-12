using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return StatusCode(result.HttpStatusCode, result);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/Order/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid order data.");

            var result = await _orderService.CreateAsync(order);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/Order/update
        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Order order)
        {
            if (!ModelState.IsValid || id <= 0 || order.OrderID != id)
                return BadRequest("Invalid order data.");

            var result = await _orderService.UpdateAsync(order);
            return StatusCode(result.HttpStatusCode, result);
        }


        // POST: api/Order/delete/5
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _orderService.DeleteAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }
    }
}
