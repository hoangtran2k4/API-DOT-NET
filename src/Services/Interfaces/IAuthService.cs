using MyApi.DTOs;
using MyApi.Models;
namespace MyApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateUserAsync(LoginDto login);

        Task UpdateUserActivity(string username);
        Task SetUserActiveStatus(string username, bool isActive);

        string GenerateJwtToken(string username, string role);
        Task<Users?> GetUserByUsernameAsync(string username);

    }
}
