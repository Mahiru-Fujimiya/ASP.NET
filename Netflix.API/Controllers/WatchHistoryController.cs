using Microsoft.AspNetCore.Mvc;
using Netflix.Business.Services;

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchHistoryController : ControllerBase
{
    private readonly IWatchHistoryService _historyService;
    public WatchHistoryController(IWatchHistoryService historyService)
        => _historyService = historyService;

    [HttpGet("{userId}")]
    public IActionResult GetHistory(int userId)
        => Ok(_historyService.GetUserHistory(userId));

    [HttpPost]
    public IActionResult AddToHistory(int userId, int movieId)
        => Ok(_historyService.AddToHistory(userId, movieId));
}