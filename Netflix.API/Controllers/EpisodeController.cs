using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.Business.Services;
using Netflix.Data.Entities;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/episodes")]
[Tags("Episodes - Quản lý Tập phim")]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeService _episodeService;
    public EpisodeController(IEpisodeService episodeService) => _episodeService = episodeService;

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetEpisodesByMovie(int movieId)
    {
        return Ok(await _episodeService.GetEpisodesByMovieAsync(movieId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEpisode(int id)
    {
        var episode = await _episodeService.GetEpisodeByIdAsync(id);
        if (episode == null) return NotFound();
        return Ok(episode);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateEpisode([FromBody] EpisodeCreateDto model)
    {
        var episode = new Episode
        {
            MovieId = model.MovieId,
            Title = model.Title,
            EpisodeNumber = model.EpisodeNumber,
            VideoUrl = model.VideoUrl
        };
        await _episodeService.AddEpisodeAsync(episode);
        return CreatedAtAction(nameof(GetEpisode), new { id = episode.Id }, episode);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEpisode(int id, [FromBody] EpisodeCreateDto model)
    {
        var episode = new Episode
        {
            EpisodeNumber = model.EpisodeNumber,
            VideoUrl = model.VideoUrl
        };
        await _episodeService.UpdateEpisodeAsync(id, episode);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEpisode(int id)
    {
        await _episodeService.DeleteEpisodeAsync(id);
        return NoContent();
    }
}

public class EpisodeCreateDto
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty; // Ví dụ: Tập 1
    public int EpisodeNumber { get; set; }
    public string VideoUrl { get; set; } = string.Empty; // Link M3U8 hoặc MP4
}