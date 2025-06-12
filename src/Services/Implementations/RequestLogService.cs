using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class RequestLogService : IRequestLogService
    {
        private readonly AppDbContext _context;

        public RequestLogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<IEnumerable<RequestLog>>> GetAllAsync()
        {
            var list = await _context.RequestLogs.ToListAsync();

            return new ApiResponse<IEnumerable<RequestLog>>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = list,
                TotalCount = list.Count,
                Message = "Fetched all request logs successfully"
            };
        }

        public async Task<ApiResponse<RequestLog?>> GetByIdAsync(long id)
        {
            var log = await _context.RequestLogs.FindAsync(id);

            return new ApiResponse<RequestLog?>
            {
                Success = log != null,
                HttpStatusCode = log != null ? 200 : 404,
                Data = log,
                Message = log != null ? "Request log found" : "Request log not found"
            };
        }

        public async Task<ApiResponse<RequestLog>> CreateAsync(RequestLog log)
        {
            await _context.RequestLogs.AddAsync(log);
            await _context.SaveChangesAsync();

            return new ApiResponse<RequestLog>
            {
                Success = true,
                HttpStatusCode = 201,
                Data = log,
                Message = "Request log created successfully"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(RequestLog log)
        {
            var existing = await _context.RequestLogs.FindAsync(log.RequestID);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Data = false,
                    Message = "Request log not found"
                };
            }

            _context.Entry(existing).CurrentValues.SetValues(log);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Request log updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            var existing = await _context.RequestLogs.FindAsync(id);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    HttpStatusCode = 404,
                    Data = false,
                    Message = "Request log not found"
                };
            }

            _context.RequestLogs.Remove(existing);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                HttpStatusCode = 200,
                Data = true,
                Message = "Request log deleted successfully"
            };
        }
    }
}
