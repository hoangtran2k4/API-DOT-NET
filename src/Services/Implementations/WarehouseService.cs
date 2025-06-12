using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<WarehouseDto>>> GetAllAsync()
        {
            var list = await _context.Warehouses
                .Select(w => new WarehouseDto
                {
                    WarehouseID = w.WarehouseID,
                    Name = w.Name,
                    Province = w.Province,
                    Capacity = w.Capacity,
                    IsActive = w.IsActive
                }).ToListAsync();

            return new ApiResponse<IEnumerable<WarehouseDto>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = list,
                TotalCount = list.Count,
                Message = "Fetched all warehouses successfully"
            };
        }

        public async Task<ApiResponse<WarehouseDto?>> GetByIdAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return new ApiResponse<WarehouseDto?>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Warehouse not found",
                    Data = null
                };
            }

            var dto = new WarehouseDto
            {
                WarehouseID = warehouse.WarehouseID,
                Name = warehouse.Name,
                Province = warehouse.Province,
                Capacity = warehouse.Capacity,
                IsActive = warehouse.IsActive
            };

            return new ApiResponse<WarehouseDto?>
            {
                Success = true,
                HttpStatusCode = 200,
                Message = "Warehouse found",
                Data = dto
            };
        }

        public async Task<ApiResponse<WarehouseDto>> CreateAsync(WarehouseDto dto)
        {
            var warehouse = new Warehouse
            {
                Name = dto.Name,
                Province = dto.Province,
                Capacity = dto.Capacity,
                IsActive = dto.IsActive
            };

            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();

            dto.WarehouseID = warehouse.WarehouseID;

            return new ApiResponse<WarehouseDto>
            {
                Success = true,
                HttpStatusCode = 201,
                Message = "Warehouse created successfully",
                Data = dto
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(WarehouseDto dto)
        {
            var warehouse = await _context.Warehouses.FindAsync(dto.WarehouseID);
            if (warehouse == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Warehouse not found",
                    Data = false
                };
            }

            warehouse.Name = dto.Name;
            warehouse.Province = dto.Province;
            warehouse.Capacity = dto.Capacity;
            warehouse.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Message = "Warehouse updated successfully",
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Warehouse not found",
                    Data = false
                };
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Message = "Warehouse deleted successfully",
                Data = true
            };
        }
    }
}
