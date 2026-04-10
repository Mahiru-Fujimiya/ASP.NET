using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Chỉ Admin (Boss Phúc) mới được vào khu vực này
[Tags("Admin - Hệ thống Quản trị & Thống kê")]
public class AdminController : ControllerBase
{
    // ==========================================
    // 🧭 DASHBOARD TỔNG QUAN
    // ==========================================
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        // Chỗ này Boss sẽ đếm tổng số User, Phim, Thể loại từ DB
        return Ok(new
        {
            totalUsers = 1500,
            totalMovies = 200,
            totalGenres = 15,
            recentViews = 5000
        });
    }

    // ==========================================
    // 👤 QUẢN LÝ USER TẬP TRUNG
    // ==========================================
    [HttpGet("users")]
    public IActionResult GetAllUsers() => Ok(new { message = "Danh sách toàn bộ User cho Admin" });

    [HttpDelete("users/{id}")]
    public IActionResult ForceDeleteUser(int id) => NoContent();

    // ==========================================
    // 📁 HỆ THỐNG UPLOAD (IMAGE & VIDEO)
    // ==========================================
    [HttpPost("upload/image")]
    [Consumes("multipart/form-data")] // Ép Swagger hiện nút chọn File
    public IActionResult UploadImage(IFormFile file)
    {
        if (file == null) return BadRequest("Boss chưa chọn ảnh!");
        // Logic lưu file vào wwwroot/posters
        return Ok(new { url = $"/posters/{file.FileName}" });
    }

    [HttpPost("upload/video")]
    [Consumes("multipart/form-data")]
    public IActionResult UploadVideo(IFormFile file)
    {
        if (file == null) return BadRequest("Boss chưa chọn video!");
        // Logic lưu file vào wwwroot/videos
        return Ok(new { url = $"/videos/{file.FileName}" });
    }

    // ==========================================
    // 📊 THỐNG KÊ CHI TIẾT (STATS)
    // ==========================================
    [HttpGet("stats/revenue")]
    public IActionResult GetRevenueStats() => Ok(new { message = "Thống kê doanh thu/lượt xem" });

    [HttpGet("stats/top-users")]
    public IActionResult GetTopUsers() => Ok(new { message = "Top người xem nhiều nhất" });
}