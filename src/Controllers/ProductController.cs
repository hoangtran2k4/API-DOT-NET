// Controllers/ProductController.cs
// API endpoint (RESTful)
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
// using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_productService.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetById(id);
        return product != null ? Ok(product) : NotFound();
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateProduct(ProductDto dto)
    {
        var result = _productService.Create(dto);

        if (!result.Success)
        {
            return result.ErrorCode switch
            {
                "SKU_REQUIRED" => BadRequest(result),
                "SKU_DUPLICATE" => Conflict(result),
                _ => StatusCode(500, result)
            };
        }

        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Update(int id, ProductDto dto)
    {
        return _productService.Update(id, dto) ? NoContent() : NotFound();
    }


    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _productService.Delete(id) ? NoContent() : NotFound();
    }
}
