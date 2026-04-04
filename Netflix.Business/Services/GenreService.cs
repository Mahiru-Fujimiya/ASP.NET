using Netflix.Data.Context;

namespace Netflix.Business.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;
    public GenreService(AppDbContext context) => _context = context;

    public object GetAllGenres() => _context.Genres.ToList();
}