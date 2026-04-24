using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public interface IEpisodeService
{
    Task<List<Episode>> GetEpisodesByMovieAsync(int movieId);
    Task<Episode?> GetEpisodeByIdAsync(int id);
    Task AddEpisodeAsync(Episode episode);
    Task UpdateEpisodeAsync(int id, Episode episode);
    Task DeleteEpisodeAsync(int id);
}
