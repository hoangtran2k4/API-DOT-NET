using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;
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
        public async Task SetUserActiveStatus(string username, bool isActive)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == username);
            if (user != null)
            {
                user.IsOnline = isActive;
                user.LastActive = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<string?> AuthenticateUserAsync(LoginDto login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.username == login.Username && u.password == login.Password);

            if (user == null) return null;

            user.LastActive = DateTime.UtcNow;
            user.IsOnline = true;
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user.username, user.role);
        }
        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.username == username);
        }

        public string GenerateJwtToken(string username, string role)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task UpdateUserActivity(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == username);
            if (user != null)
            {
                user.LastActive = DateTime.UtcNow;
                user.IsOnline = true;
                await _context.SaveChangesAsync();
            }
        }
    }

    public class UserOnlineChecker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserOnlineChecker> _logger;

        public UserOnlineChecker(IServiceScopeFactory scopeFactory, ILogger<UserOnlineChecker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    var timeout = DateTime.UtcNow.AddMinutes(-5);

                    var inactiveUsers = db.Users
                        .Where(u => u.IsOnline && (u.LastActive == null || u.LastActive < timeout));

                    foreach (var user in inactiveUsers)
                        user.IsOnline = false;

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while checking user online status.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

    }

}
