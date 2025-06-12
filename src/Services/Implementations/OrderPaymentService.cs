using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class OrderPaymentService : IOrderPaymentService
    {
        private readonly AppDbContext _context;

        public OrderPaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<OrderPayments>>> GetAllAsync()
        {
            var payments = await _context.OrderPayments.ToListAsync();

            return new ApiResponse<IEnumerable<OrderPayments>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = payments,
                TotalCount = payments.Count,
                Message = "Fetched all order payments successfully"
            };
        }

        public async Task<ApiResponse<OrderPayments?>> GetByIdAsync(long id)
        {
            var payment = await _context.OrderPayments.FindAsync(id);

            return new ApiResponse<OrderPayments?>
            {
                Success = payment != null,
                HttpStatusCode = payment != null ? 200 : 404,
                Data = payment,
                Message = payment != null ? "Order payment found" : "Order payment not found"
            };
        }

        public async Task<ApiResponse<OrderPayments>> CreateAsync(OrderPayments orderPayment)
        {
            await _context.OrderPayments.AddAsync(orderPayment);
            await _context.SaveChangesAsync();

            return new ApiResponse<OrderPayments>
            {
                Success = true,
                HttpStatusCode = 201,
                Data = orderPayment,
                Message = "Order payment created successfully"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(OrderPayments orderPayment)
        {
            var existing = await _context.OrderPayments.FindAsync(orderPayment.PaymentID);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order payment not found",
                    Data = false
                };
            }

            _context.Entry(existing).CurrentValues.SetValues(orderPayment);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order payment updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            var payment = await _context.OrderPayments.FindAsync(id);
            if (payment == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Message = "Order payment not found",
                    Data = false
                };
            }

            _context.OrderPayments.Remove(payment);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Order payment deleted successfully"
            };
        }
    }
}
