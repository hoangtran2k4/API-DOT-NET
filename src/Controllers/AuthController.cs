using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var token = await _authService.AuthenticateUserAsync(login);
        if (token == null)
        {
            return Unauthorized(new { message = "Thông tin đăng nhập không đúng" });
        }
        return Ok(new { token });
    }
}
