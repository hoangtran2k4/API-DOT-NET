using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestLogController : ControllerBase
    {
        private readonly IRequestLogService _requestLogService;

        public RequestLogController(IRequestLogService requestLogService)
        {
            _requestLogService = requestLogService;
        }

        // GET: api/RequestLog
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _requestLogService.GetAllAsync();
            return StatusCode(result.HttpStatusCode, result);
        }

        // GET: api/RequestLog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _requestLogService.GetByIdAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/RequestLog/create
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RequestLog log)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request log data.");

            var result = await _requestLogService.CreateAsync(log);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/RequestLog/update
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] RequestLog log)
        {
            if (!ModelState.IsValid || log.RequestID <= 0)
                return BadRequest("Invalid request log data.");

            var result = await _requestLogService.UpdateAsync(log);
            return StatusCode(result.HttpStatusCode, result);
        }

        // POST: api/RequestLog/delete/5
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _requestLogService.DeleteAsync(id);
            return StatusCode(result.HttpStatusCode, result);
        }
    }
}
