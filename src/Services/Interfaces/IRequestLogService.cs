using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IRequestLogService
    {
        Task<ApiResponse<IEnumerable<RequestLog>>> GetAllAsync();
        Task<ApiResponse<RequestLog?>> GetByIdAsync(long id);
        Task<ApiResponse<RequestLog>> CreateAsync(RequestLog log);
        Task<ApiResponse<bool>> UpdateAsync(RequestLog log);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
