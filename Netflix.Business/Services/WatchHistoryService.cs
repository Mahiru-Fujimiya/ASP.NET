using Netflix.Data.Context;
using Netflix.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Netflix.Business.Services;

public class WatchHistoryService : IWatchHistoryService
{
    private readonly AppDbContext _context;
    public WatchHistoryService(AppDbContext context) => _context = context;

    public object AddToHistory(int userId, int movieId)
    {
        var history = new WatchHistory
        {
            UserId = userId,
            MovieId = movieId,
            WatchedAt = DateTime.Now
        };
        _context.WatchHistories.Add(history);
        _context.SaveChanges();
        return new { message = "Đã lưu vào lịch sử" };
    }

    public object GetUserHistory(int userId)
    {
        return _context.WatchHistories
                       .Where(h => h.UserId == userId)
                       .Include(h => h.Movie)
                       .OrderByDescending(h => h.WatchedAt)
                       .ToList();
    }
}