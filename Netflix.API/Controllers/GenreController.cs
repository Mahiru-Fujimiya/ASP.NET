using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.Business.Services;
using Netflix.Data.Entities;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/genre")]
[Tags("Genres - Quản lý Thể loại")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService genreService) => _genreService = genreService;

    [HttpGet]
    public async Task<IActionResult> GetGenres() => Ok(await _genreService.GetAllGenresAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre == null) return NotFound();
        return Ok(genre);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreDto model)
    {
        var genre = new Genre { Name = model.Name, Description = model.Description };
        await _genreService.AddGenreAsync(genre);
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteGenreAsync(id);
        return NoContent();
    }
}

public class GenreDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}