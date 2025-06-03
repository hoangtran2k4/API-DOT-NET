// Models/Product.cs
namespace MyApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Sku { get; set; } = null!;
}
