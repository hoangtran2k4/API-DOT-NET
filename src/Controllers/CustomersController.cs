using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;

namespace MyApi.Controllers;

using Microsoft.AspNetCore.Authorization;

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
    public IActionResult GetAll() => Ok(_customersService.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var customer = _customersService.GetById(id);
        return customer != null ? Ok(customer) : NotFound();
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateCustomer(CustomerDto dto)
    {
        var result = _customersService.CreateCustomer(dto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Update(long id, CustomerDto dto) => Ok(_customersService.Update(id, dto));

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(long id) => Ok(_customersService.Delete(id));
}
