using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Models;
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
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                HttpStatusCode = 401,
                Message = "Thông tin đăng nhập không đúng",
                Data = null
            });
        }

        return Ok(new ApiResponse<string>
        {
            Success = true,
            HttpStatusCode = 200,
            Message = "Đăng nhập thành công",
            Data = token
        });
    }

    [Authorize]
    [HttpPost("update-activity")]
    public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityDto model)
    {
        var username = User.Identity?.Name;
        if (username == null || username != model.Username) return Unauthorized();

        await _authService.SetUserActiveStatus(username, model.IsActive);

        return Ok(new
        {
            success = true,
            status = model.IsActive ? "active" : "inactive",
            time = DateTime.UtcNow
        });
    }
    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var username = User.Identity?.Name;
        if (username == null) return Unauthorized();

        var user = await _authService.GetUserByUsernameAsync(username);
        if (user == null) return Unauthorized();

        if (user.LastActive == null || user.LastActive < DateTime.UtcNow.AddMinutes(-5))// không hoạt động sau 5 phút sẽ không gia hạn thêm thoken mới
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                HttpStatusCode = 401,
                Message = "Phiên làm việc đã hết hạn, vui lòng đăng nhập lại.",
                Data = null
            });
        }

        await _authService.UpdateUserActivity(username);

        var newToken = _authService.GenerateJwtToken(user.username, user.role);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            HttpStatusCode = 200,
            Message = "Gia hạn token thành công",
            Data = newToken
        });
    }
}
