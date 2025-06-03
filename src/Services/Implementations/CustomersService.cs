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

    public IEnumerable<Customer> GetAll() => _context.Customers.ToList();

    public Customer? GetById(long id) => _context.Customers.Find(id);

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
            Data = customer,
            Message = "Tạo mới thành công"
        };
    }

    public Customer Update(long id, CustomerDto dto)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) throw new Exception("Customer not found");

        customer.FullName = dto.FullName;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
        customer.Gender = dto.Gender;
        customer.BirthDate = dto.BirthDate;
        customer.Channel = dto.Channel;
        customer.IsMember = dto.IsMember;
        customer.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return customer;
    }

    public bool Delete(long id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return true;
    }
}