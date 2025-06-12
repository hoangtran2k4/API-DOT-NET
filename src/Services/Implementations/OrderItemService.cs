using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;

        public OrderItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<OrderItem>>> GetAllAsync()
        {
            var items = await _context.OrderItems.ToListAsync();

            return new ApiResponse<IEnumerable<OrderItem>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = items,
                TotalCount = items.Count,
                Message = "Fetched all order items successfully"
            };
        }

        public async Task<ApiResponse<OrderItem?>> GetByIdAsync(long id)
        {
            var item = await _context.OrderItems.FindAsync(id);

            return new ApiResponse<OrderItem?>
            {
                Success = item != null,
                HttpStatusCode = item != null ? 200 : 404,
                Data = item,
                Message = item != null ? "Order item found" : "Order item not found"
            };
        }

        public async Task<ApiResponse<OrderItem>> CreateAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<OrderItem>
            {
                Success = true,
                HttpStatusCode = 201,
                Data = orderItem,
                Message = "Order item created successfully"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(OrderItem orderItem)
        {
            var existing = await _context.OrderItems.FindAsync(orderItem.OrderItemID);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order item not found",
                    Data = false
                };
            }

            _context.Entry(existing).CurrentValues.SetValues(orderItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order item updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order item not found",
                    Data = false
                };
            }

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order item deleted successfully"
            };
        }
    }
}
