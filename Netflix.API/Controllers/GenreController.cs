using Microsoft.AspNetCore.Mvc;
using Netflix.Business.Services;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService genreService) => _genreService = genreService;

    [HttpGet]
    public IActionResult GetAll() => Ok(_genreService.GetAllGenres());
}