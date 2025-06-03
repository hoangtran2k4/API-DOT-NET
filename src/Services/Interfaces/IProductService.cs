// IProductService.cs
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        ApiResponse<Product> Create(ProductDto dto);

        bool Update(int id, ProductDto dto);
        bool Delete(int id);
    }
}
