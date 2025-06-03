// DTOs/ProductDto.cs
// DTO dùng cho create/update
namespace MyApi.DTOs;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Sku { get; set; } = null!;
}
