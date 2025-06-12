using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class OrderShippingService : IOrderShippingService
    {
        private readonly AppDbContext _context;

        public OrderShippingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<OrderShipping>>> GetAllAsync()
        {
            var list = await _context.OrderShippings.ToListAsync();

            return new ApiResponse<IEnumerable<OrderShipping>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = list,
                TotalCount = list.Count,
                Message = "Fetched all order shippings successfully"
            };
        }

        public async Task<ApiResponse<OrderShipping?>> GetByIdAsync(long id)
        {
            var item = await _context.OrderShippings.FindAsync(id);

            return new ApiResponse<OrderShipping?>
            {
                Success = item != null,
                HttpStatusCode = item != null ? 200 : 404,
                Data = item,
                Message = item != null ? "Order shipping found" : "Order shipping not found"
            };
        }

        public async Task<ApiResponse<OrderShipping>> CreateAsync(OrderShipping orderShipping)
        {
            await _context.OrderShippings.AddAsync(orderShipping);
            await _context.SaveChangesAsync();

            return new ApiResponse<OrderShipping>
            {
                Success = true,
                HttpStatusCode = 201,
                Data = orderShipping,
                Message = "Order shipping created successfully"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(OrderShipping orderShipping)
        {
            var existing = await _context.OrderShippings.FindAsync(orderShipping.ShippingID);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order shipping not found",
                    Data = false
                };
            }

            _context.Entry(existing).CurrentValues.SetValues(orderShipping);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order shipping updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            var item = await _context.OrderShippings.FindAsync(id);
            if (item == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order shipping not found",
                    Data = false
                };
            }

            _context.OrderShippings.Remove(item);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order shipping deleted successfully"
            };
        }
    }
}
