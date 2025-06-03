// Services/ProductService.cs
// Triển khai IProductService
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll() => _context.Products.ToList();

    public Product? GetById(int id) => _context.Products.Find(id);

    public ApiResponse<Product> Create(ProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Sku))
        {
            return new ApiResponse<Product>
            {
                Success = false,
                Data = null,
                Message = "Sku không được để trống.",
                ErrorCode = "SKU_REQUIRED"
            };
        }

        bool skuExists = _context.Products.Any(p => p.Sku == dto.Sku);
        if (skuExists)
        {
            return new ApiResponse<Product>
            {
                Success = false,
                Data = null,
                Message = $"Sku '{dto.Sku}' đã tồn tại.",
                ErrorCode = "SKU_DUPLICATE"
            };
        }

        try
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Sku = dto.Sku
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ApiResponse<Product>
            {
                Success = true,
                Data = product,
                Message = "Tạo sản phẩm thành công",
                ErrorCode = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Product>
            {
                Success = false,
                Data = null,
                Message = "Lỗi hệ thống khi tạo sản phẩm: " + ex.Message,
                ErrorCode = "CREATE_PRODUCT_FAILED"
            };
        }
    }



    public bool Update(int id, ProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Sku = dto.Sku;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}


