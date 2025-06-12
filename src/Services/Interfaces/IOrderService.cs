using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<IEnumerable<Order>>> GetAllAsync();
        Task<ApiResponse<Order?>> GetByIdAsync(long id);
        Task<ApiResponse<Order>> CreateAsync(Order order);
        Task<ApiResponse<bool>> UpdateAsync(Order order);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
