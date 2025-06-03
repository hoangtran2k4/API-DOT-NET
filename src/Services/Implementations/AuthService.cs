using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MyApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<string?> AuthenticateUserAsync(LoginDto login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.username == login.Username && u.password == login.Password);

            if (user == null) return null;
            return GenerateJwtToken(user.username);
        }
        private string GenerateJwtToken(string username)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            };
            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwt["ExpiresInMinutes"]!)),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
