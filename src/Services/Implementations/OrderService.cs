using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<Order>>> GetAllAsync()
        {
            var data = await _context.Orders.ToListAsync();
            return new ApiResponse<IEnumerable<Order>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = data,
                TotalCount = data.Count
            };
        }

        public async Task<ApiResponse<Order?>> GetByIdAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            return new ApiResponse<Order?>
            {
                Success = order != null,
                HttpStatusCode = order != null ? 200 : 404,
                Data = order,
                Message = order != null ? null : "Order not found"
            };
        }

        public async Task<ApiResponse<Order>> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return new ApiResponse<Order>
            {
                Success = true,
                HttpStatusCode = 201,
                Data = order
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            var affected = await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Success = affected > 0,
                HttpStatusCode = affected > 0 ? 200 : 400,
                Data = affected > 0
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return new ApiResponse<bool> { Success = false, HttpStatusCode = 404, Message = "Order not found" };
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool> { Success = true, HttpStatusCode = 200, Data = true };
        }
    }
}
