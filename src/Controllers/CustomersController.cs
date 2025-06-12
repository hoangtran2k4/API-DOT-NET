using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;

namespace MyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _customersService.GetAll();
        return StatusCode(result.HttpStatusCode, result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var result = _customersService.GetById(id);
        return StatusCode(result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CustomerDto dto)
    {
        var result = _customersService.CreateCustomer(dto);
        return StatusCode(result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpPost("{id}/put")]
    public IActionResult Update(long id, [FromBody] CustomerDto dto)
    {
        var result = _customersService.Update(id, dto);
        return StatusCode(result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpPost("{id}/delete")]
    public IActionResult Delete(long id)
    {
        var result = _customersService.Delete(id);
        return StatusCode(result.HttpStatusCode, result);
    }
}
