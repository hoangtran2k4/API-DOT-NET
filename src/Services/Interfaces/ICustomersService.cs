using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface ICustomersService
    {
        IEnumerable<Customer> GetAll();
        Customer? GetById(long id);
        ApiResponse<Customer> CreateCustomer(CustomerDto dto);
        Customer Update(long id, CustomerDto dto);
        bool Delete(long id);

    }
}