using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;
using Netflix.Business.Services;
using Netflix.Data.Entities;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Auth - Quản lý Xác thực")]
[EnableRateLimiting("fixed")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // ==========================================
    // 📝 ĐĂNG KÝ TÀI KHOẢN
    // ==========================================
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = model.Password,
            Role = "User"
        };
        var result = await _authService.RegisterAsync(user);
        if (!result) return BadRequest("Username đã tồn tại!");
        return Ok(new { message = "Đăng ký thành công!" });
    }

    // ==========================================
    // 🔑 ĐĂNG NHẬP
    // ==========================================
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var result = await _authService.LoginAsync(model.Username, model.Password);
        if (result == null) return Unauthorized("Tài khoản hoặc mật khẩu không chính xác!");
        return Ok(result);
    }

    // ==========================================
    // 🚪 ĐĂNG XUẤT
    // ==========================================
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { message = "Đã đăng xuất khỏi hệ thống." });
    }

    // ==========================================
    // 🔄 LÀM MỚI TOKEN
    // ==========================================
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        return Ok(new { token = "new-access-token" });
    }

    // ==========================================
    // 👤 THÔNG TIN CÁ NHÂN (Yêu cầu Login)
    // ==========================================
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(new { id = userId, username = "Lawwy", email = "phuc@gmail.com" });
    }

    // ==========================================
    // 🔐 ĐỔI MẬT KHẨU
    // ==========================================
    [Authorize]
    [HttpPut("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest model)
    {
        return NoContent();
    }
}

// --- DTOs (Boss có thể tách ra file riêng trong thư mục Models/DTOs) ---

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ChangePasswordRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}