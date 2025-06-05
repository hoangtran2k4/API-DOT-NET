using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services;

public class CustomersService : ICustomersService
{
    private readonly AppDbContext _context;

    public CustomersService(AppDbContext context)
    {
        _context = context;
    }

    public ApiResponse<IEnumerable<Customer>> GetAll()
    {
        var customers = _context.Customers.ToList();
        return new ApiResponse<IEnumerable<Customer>>
        {
            Success = true,
            HttpStatusCode = 200,
            Data = customers,
            TotalCount = customers.Count
        };
    }

    public ApiResponse<Customer?> GetById(long id)
    {
        var customer = _context.Customers.Find(id);
        return new ApiResponse<Customer?>
        {
            Success = customer != null,
            HttpStatusCode = customer != null ? 200 : 404,
            Message = customer != null ? "Tìm thấy khách hàng" : "Không tìm thấy",
            Data = customer,
            TotalCount = customer != null ? 1 : 0
        };
    }

    public ApiResponse<Customer> CreateCustomer(CustomerDto dto)
    {
        var customer = new Customer
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            Channel = dto.Channel,
            IsMember = dto.IsMember,
            CreatedAt = DateTime.Now
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return new ApiResponse<Customer>
        {
            Success = true,
            HttpStatusCode = 201,
            Message = "Tạo khách hàng thành công",
            Data = customer,
            TotalCount = 1
        };
    }

    public ApiResponse<Customer> Update(long id, CustomerDto dto)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return new ApiResponse<Customer>
            {
                Success = false,
                HttpStatusCode = 404,
                Message = "Không tìm thấy khách hàng",
                Data = null,
                TotalCount = 0
            };
        }

        customer.FullName = dto.FullName;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
        customer.Gender = dto.Gender;
        customer.BirthDate = dto.BirthDate;
        customer.Channel = dto.Channel;
        customer.IsMember = dto.IsMember;
        customer.UpdatedAt = DateTime.Now;

        _context.SaveChanges();

        return new ApiResponse<Customer>
        {
            Success = true,
            HttpStatusCode = 200,
            Message = "Cập nhật thành công",
            Data = customer,
            TotalCount = 1
        };
    }

    public ApiResponse<bool> Delete(long id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                HttpStatusCode = 404,
                Message = "Không tìm thấy khách hàng",
                Data = false
            };
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return new ApiResponse<bool>
        {
            Success = true,
            HttpStatusCode = 200,
            Message = "Xóa khách hàng thành công",
            Data = true
        };
    }
}
