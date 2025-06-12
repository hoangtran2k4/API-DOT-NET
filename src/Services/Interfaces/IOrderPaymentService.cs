using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IOrderPaymentService
    {
        Task<ApiResponse<IEnumerable<OrderPayments>>> GetAllAsync();
        Task<ApiResponse<OrderPayments?>> GetByIdAsync(long id);
        Task<ApiResponse<OrderPayments>> CreateAsync(OrderPayments orderPayment);
        Task<ApiResponse<bool>> UpdateAsync(OrderPayments orderPayment);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
