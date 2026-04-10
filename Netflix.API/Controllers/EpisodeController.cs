using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Episodes - Quản lý Tập phim")]
public class EpisodeController : ControllerBase
{
    // ==========================================
    // 🎞️ LẤY DANH SÁCH TẬP THEO PHIM
    // ==========================================
    [HttpGet("movie/{movieId}")]
    public IActionResult GetEpisodesByMovie(int movieId)
    {
        return Ok(new { message = $"Danh sách tập của phim ID: {movieId}" });
    }

    [HttpGet("{id}")]
    public IActionResult GetEpisode(int id) => Ok();

    // ==========================================
    // 🔐 QUẢN TRỊ TẬP PHIM (Admin Only)
    // ==========================================
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateEpisode([FromBody] EpisodeCreateDto model) => Created("", model);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateEpisode(int id, [FromBody] EpisodeCreateDto model) => NoContent();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteEpisode(int id) => NoContent();
}

public class EpisodeCreateDto
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty; // Ví dụ: Tập 1
    public int EpisodeNumber { get; set; }
    public string VideoUrl { get; set; } = string.Empty; // Link M3U8 hoặc MP4
}