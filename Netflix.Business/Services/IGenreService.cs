using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public interface IGenreService
{
    Task<List<Genre>> GetAllGenresAsync();
    Task<Genre?> GetGenreByIdAsync(int id);
    Task AddGenreAsync(Genre genre);
    Task DeleteGenreAsync(int id);
}