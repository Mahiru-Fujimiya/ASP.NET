using Microsoft.AspNetCore.Mvc;
using Netflix.Data.Context;

[ApiController]
[Route("api/[controller]")]
public class EpisodeController : ControllerBase
{
    private readonly AppDbContext _context;
    public EpisodeController(AppDbContext context) => _context = context;

    [HttpGet("movie/{movieId}")] // Lấy tất cả tập của 1 bộ phim
    public IActionResult GetByMovie(int movieId)
    {
        var episodes = _context.Episodes
                               .Where(e => e.MovieId == movieId)
                               .ToList();
        return Ok(episodes);
    }
}