using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<ApiResponse<IEnumerable<OrderItem>>> GetAllAsync();
        Task<ApiResponse<OrderItem?>> GetByIdAsync(long id);
        Task<ApiResponse<OrderItem>> CreateAsync(OrderItem orderItem);
        Task<ApiResponse<bool>> UpdateAsync(OrderItem orderItem);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
