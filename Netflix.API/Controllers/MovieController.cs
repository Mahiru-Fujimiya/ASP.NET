using Microsoft.AspNetCore.Mvc;
using Netflix.Business.Services;

namespace Netflix.API.Controllers;

[ApiController]

[Route("api/[controller]")] // Đường dẫn sẽ là api/movie [cite: 387, 476]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet] // GET /api/movie [cite: 396, 476]
    public IActionResult GetAll()
    {
        var movies = _movieService.GetMovies();
        return Ok(movies);
    }

    
    [HttpGet("{id}")] // GET /api/movie/{id} [cite: 398, 489]
    public IActionResult GetById(int id)
    {
        var movie = _movieService.GetMovieById(id);
        if (movie == null) return NotFound(new { message = "Không tìm thấy phim" });
        return Ok(movie);
    }

    // GET /api/Movie/search?title=John
    [HttpGet("search")]
    public IActionResult Search(string title)
    {
        var results = _movieService.SearchMovies(title);
        return Ok(results);
    }

    // DELETE /api/Movie/{id} - Dùng để xóa phim lỗi
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // Code xóa phim thực tế ở đây
        return Ok(new { message = $"Đã xóa phim có ID {id}" });
    }
}