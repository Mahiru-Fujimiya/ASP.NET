using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.Business.Services;
using Netflix.Data.Entities;
using System.IO;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[Tags("Admin - Hệ thống Quản trị & Thống kê")]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly IWebHostEnvironment _env;

    public AdminController(IUserService userService, IMovieService movieService, IGenreService genreService, IWebHostEnvironment env)
    {
        _userService = userService;
        _movieService = movieService;
        _genreService = genreService;
        _env = env;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var users = await _userService.GetAllUsersAsync();
        var (movies, _) = await _movieService.GetMoviesAsync(null, null, null, 1, 1000);
        var genres = await _genreService.GetAllGenresAsync();

        return Ok(new
        {
            totalUsers = users.Count,
            totalMovies = movies.Count,
            totalGenres = genres.Count,
            recentViews = 5000 // Placeholder hoặc lấy từ WatchHistoryService
        });
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers() => Ok(await _userService.GetAllUsersAsync());

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> ForceDeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPost("upload/image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Boss chưa chọn ảnh!");

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine(_env.WebRootPath, "posters", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { url = $"/posters/{fileName}" });
    }

    [HttpPost("upload/video")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVideo(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Boss chưa chọn video!");

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine(_env.WebRootPath, "videos", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { url = $"/videos/{fileName}" });
    }

    [HttpGet("stats/revenue")]
    public IActionResult GetRevenueStats() => Ok(new { message = "Thống kê doanh thu/lượt xem" });

    [HttpGet("stats/top-users")]
    public IActionResult GetTopUsers() => Ok(new { message = "Top người xem nhiều nhất" });
}