using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Movies - Quản lý Kho phim")] // Tách bảng riêng trên Swagger
public class MovieController : ControllerBase
{
    // ==========================================
    // 🎥 LẤY DANH SÁCH PHIM (Có phân trang & lọc)
    // ==========================================
    [HttpGet]
    public IActionResult GetMovies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? keyword = null,
        [FromQuery] int? genreId = null)
    {
        return Ok(new { message = "Danh sách phim của Boss Phúc", page, pageSize });
    }

    [HttpGet("{id}")]
    public IActionResult GetMovie(int id) => Ok(new { id, title = "Phim mẫu" });

    // ==========================================
    // 🔐 QUẢN TRỊ (Chỉ Admin mới được dùng)
    // ==========================================
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateMovie([FromBody] MovieCreateDto model)
        => CreatedAtAction(nameof(GetMovie), new { id = 1 }, model);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] MovieCreateDto model) => NoContent();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id) => NoContent();

    // ==========================================
    // ⭐ XU HƯỚNG & GỢI Ý
    // ==========================================
    [HttpGet("trending")]
    public IActionResult GetTrending() => Ok();

    [HttpGet("top-rated")]
    public IActionResult GetTopRated() => Ok();

    // ==========================================
    // ❤️ YÊU THÍCH (Yêu cầu Login)
    // ==========================================
    [Authorize]
    [HttpPost("{id}/favorite")]
    public IActionResult AddFavorite(int id) => Ok("Đã thêm vào danh sách yêu thích");

    [Authorize]
    [HttpDelete("{id}/favorite")]
    public IActionResult RemoveFavorite(int id) => NoContent();

    // ==========================================
    // 💬 BÌNH LUẬN (COMMENTS)
    // ==========================================
    [HttpGet("{id}/comments")]
    public IActionResult GetComments(int id) => Ok();

    [Authorize]
    [HttpPost("{id}/comments")]
    public IActionResult AddComment(int id, [FromBody] string content) => Ok();
}

// --- DTO dành cho Movie ---
public class MovieCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Poster { get; set; }
    public int GenreId { get; set; }
    public int ReleaseYear { get; set; }
}