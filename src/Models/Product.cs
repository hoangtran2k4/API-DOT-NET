// Models/Product.cs
namespace MyApi.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Sku { get; set; } = null!;
}
