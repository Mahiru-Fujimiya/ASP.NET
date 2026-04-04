using Netflix.Business.DTOs;

namespace Netflix.Business.Services;

public interface IWatchHistoryService
{
    object AddToHistory(int userId, int movieId);
    object GetUserHistory(int userId);
}