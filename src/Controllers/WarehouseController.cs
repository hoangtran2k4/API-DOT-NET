using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode(response.HttpStatusCode, response);
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] WarehouseDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode(response.HttpStatusCode, response);
        }
        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WarehouseDto dto)
        {
            dto.WarehouseID = id;
            var response = await _service.UpdateAsync(dto);
            return StatusCode(response.HttpStatusCode, response);
        }
        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.HttpStatusCode, response);
        }
    }
}
