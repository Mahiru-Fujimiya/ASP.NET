using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Auth - Quản lý Xác thực")] // Swagger sẽ hiển thị bảng này riêng
[EnableRateLimiting("fixed")]
public class AuthController : ControllerBase
{
    // ==========================================
    // 📝 ĐĂNG KÝ TÀI KHOẢN
    // ==========================================
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest model)
    {
        // Boss sẽ gọi AuthService.Register(model) ở đây
        return Ok(new { message = "Đăng ký thành công!" });
    }

    // ==========================================
    // 🔑 ĐĂNG NHẬP
    // ==========================================
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        // Boss sẽ xử lý kiểm tra User và tạo JWT Token ở đây
        return Ok(new
        {
            token = "jwt-token-gia-lap-cua-boss-phuc",
            username = model.Username,
            role = "Admin"
        });
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

public class RegisterRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ChangePasswordRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}