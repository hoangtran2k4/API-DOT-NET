using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPaymentController : ControllerBase
    {
        private readonly IOrderPaymentService _orderPaymentService;

        public OrderPaymentController(IOrderPaymentService orderPaymentService)
        {
            _orderPaymentService = orderPaymentService;
        }

        // GET: api/OrderPayment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderPaymentService.GetAllAsync();
            return StatusCode(result.HttpStatusCode, result);
        }

        // GET: api/OrderPayment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _orderPaymentService.GetByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderPayment/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderPayments orderPayment)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid order payment data.");

            var result = await _orderPaymentService.CreateAsync(orderPayment);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderPayment/update
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] OrderPayments orderPayment)
        {
            if (!ModelState.IsValid || orderPayment.PaymentID <= 0)
                return BadRequest("Invalid order payment data.");

            var result = await _orderPaymentService.UpdateAsync(orderPayment);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/OrderPayment/delete/5
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _orderPaymentService.DeleteAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }
    }
}
