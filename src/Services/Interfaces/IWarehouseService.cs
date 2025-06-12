using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<ApiResponse<IEnumerable<WarehouseDto>>> GetAllAsync();
        Task<ApiResponse<WarehouseDto?>> GetByIdAsync(int id);
        Task<ApiResponse<WarehouseDto>> CreateAsync(WarehouseDto dto);
        Task<ApiResponse<bool>> UpdateAsync(WarehouseDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
