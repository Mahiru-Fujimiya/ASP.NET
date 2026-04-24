using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.Business.Services;
using Netflix.Data.Entities;
using System.IO;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/movies")]
[Tags("Movies - Quản lý Kho phim")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService) => _movieService = movieService;

    [HttpGet]
    public async Task<IActionResult> GetMovies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? keyword = null,
        [FromQuery] int? genreId = null,
        [FromQuery] int? releaseYear = null)
    {
        var (items, totalCount) = await _movieService.GetMoviesAsync(keyword, genreId, releaseYear, page, pageSize);
        return Ok(new { data = items, totalCount, page, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null) return NotFound("Không tìm thấy phim!");
        return Ok(movie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto model)
    {
        var movie = new Movie
        {
            Title = model.Title,
            Description = model.Description,
            Poster = model.Poster,
            GenreId = model.GenreId,
            ReleaseYear = model.ReleaseYear
        };
        await _movieService.AddMovieAsync(movie);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieCreateDto model)
    {
        var movie = new Movie
        {
            Title = model.Title,
            Description = model.Description,
            Poster = model.Poster,
            GenreId = model.GenreId,
            ReleaseYear = model.ReleaseYear
        };
        await _movieService.UpdateMovieAsync(id, movie);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadMovie([FromForm] string title, [FromForm] int genreId, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Chưa chọn file poster!");

        // Upload ảnh trước
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var postersDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "posters");
        if (!Directory.Exists(postersDir)) Directory.CreateDirectory(postersDir);
        
        var path = Path.Combine(postersDir, fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Lưu vào DB
        var movie = new Movie
        {
            Title = title,
            GenreId = genreId,
            Poster = "/posters/" + fileName,
            ReleaseYear = DateTime.Now.Year
        };
        await _movieService.AddMovieAsync(movie);

        return Ok(movie);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        await _movieService.DeleteMovieAsync(id);
        return NoContent();
    }

    [HttpGet("trending")]
    public async Task<IActionResult> GetTrending()
    {
        var (movies, _) = await _movieService.GetMoviesAsync(null, null, null, 1, 10);
        return Ok(movies);
    }

    [HttpGet("top-rated")]
    public async Task<IActionResult> GetTopRated()
    {
        var (movies, _) = await _movieService.GetMoviesAsync(null, null, null, 1, 10);
        // Có thể sort theo AverageRating ở đây nếu cần, nhưng hiện tại mock
        return Ok(movies.OrderByDescending(m => m.AverageRating).ToList());
    }

    [Authorize]
    [HttpPost("{id}/favorite")]
    public IActionResult AddFavorite(int id) => Ok("Đã thêm vào danh sách yêu thích");

    [Authorize]
    [HttpDelete("{id}/favorite")]
    public IActionResult RemoveFavorite(int id) => NoContent();

    [HttpGet("{id}/comments")]
    public IActionResult GetComments(int id) => Ok(new List<object>());

    [Authorize]
    [HttpPost("{id}/comments")]
    public IActionResult AddComment(int id, [FromBody] string content) => Ok();

    [Authorize]
    [HttpPost("{id}/rate")]
    public async Task<IActionResult> RateMovie(int id, [FromBody] RatingDto dto)
    {
        if (dto.Rating < 1 || dto.Rating > 5) return BadRequest("Rating must be between 1 and 5.");
        await _movieService.RateMovieAsync(id, dto.Rating);
        return Ok(new { message = "Rating successful" });
    }
}

public class RatingDto
{
    public int Rating { get; set; }
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