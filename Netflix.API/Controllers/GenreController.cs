using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Genres - Quản lý Thể loại")]
public class GenreController : ControllerBase
{
    [HttpGet]
    public IActionResult GetGenres() => Ok();

    [HttpGet("{id}")]
    public IActionResult GetGenre(int id) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateGenre([FromBody] GenreDto model) => Created("", model);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id) => NoContent();
}

public class GenreDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}