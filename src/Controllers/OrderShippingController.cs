using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderShippingController : ControllerBase
    {
        private readonly IOrderShippingService _orderShippingService;

        public OrderShippingController(IOrderShippingService orderShippingService)
        {
            _orderShippingService = orderShippingService;
        }

        // GET: api/OrderShipping
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderShippingService.GetAllAsync();
            return StatusCode(result.HttpStatusCode, result);
        }

        // GET: api/OrderShipping/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _orderShippingService.GetByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderShipping/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderShipping orderShipping)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid order shipping data.");

            var result = await _orderShippingService.CreateAsync(orderShipping);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderShipping/update
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] OrderShipping orderShipping)
        {
            if (!ModelState.IsValid || orderShipping.ShippingID <= 0)
                return BadRequest("Invalid order shipping data.");

            var result = await _orderShippingService.UpdateAsync(orderShipping);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderShipping/delete/5
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _orderShippingService.DeleteAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }
    }
}
