using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IOrderShippingService
    {
        Task<ApiResponse<IEnumerable<OrderShipping>>> GetAllAsync();
        Task<ApiResponse<OrderShipping?>> GetByIdAsync(long id);
        Task<ApiResponse<OrderShipping>> CreateAsync(OrderShipping orderShipping);
        Task<ApiResponse<bool>> UpdateAsync(OrderShipping orderShipping);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
