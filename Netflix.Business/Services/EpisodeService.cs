using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public class EpisodeService : IEpisodeService
{
    private readonly AppDbContext _context;
    public EpisodeService(AppDbContext context) => _context = context;

    public async Task<List<Episode>> GetEpisodesByMovieAsync(int movieId) =>
        await _context.Episodes.Where(e => e.MovieId == movieId).OrderBy(e => e.EpisodeNumber).ToListAsync();

    public async Task<Episode?> GetEpisodeByIdAsync(int id) =>
        await _context.Episodes.Include(e => e.Movie).FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddEpisodeAsync(Episode episode)
    {
        _context.Episodes.Add(episode);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEpisodeAsync(int id, Episode episodeUpdate)
    {
        var episode = await _context.Episodes.FindAsync(id);
        if (episode != null)
        {
            episode.EpisodeNumber = episodeUpdate.EpisodeNumber;
            episode.VideoUrl = episodeUpdate.VideoUrl;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteEpisodeAsync(int id)
    {
        var episode = await _context.Episodes.FindAsync(id);
        if (episode != null)
        {
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
        }
    }
}
