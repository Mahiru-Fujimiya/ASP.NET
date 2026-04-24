using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;
    public GenreService(AppDbContext context) => _context = context;

    public async Task<List<Genre>> GetAllGenresAsync() => await _context.Genres.ToListAsync();

    public async Task<Genre?> GetGenreByIdAsync(int id) => await _context.Genres.FindAsync(id);

    public async Task AddGenreAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}