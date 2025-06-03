using MyApi.DTOs;
namespace MyApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateUserAsync(LoginDto login);
    }
}
