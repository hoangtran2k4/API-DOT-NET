using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services
{
    public interface ICustomersService
    {
        ApiResponse<IEnumerable<Customer>> GetAll();
        ApiResponse<Customer?> GetById(long id);
        ApiResponse<Customer> CreateCustomer(CustomerDto dto);
        ApiResponse<Customer> Update(long id, CustomerDto dto);
        ApiResponse<bool> Delete(long id);
    }
}
