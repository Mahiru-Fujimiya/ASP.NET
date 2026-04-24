using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public class MovieService : IMovieService
{
    private readonly AppDbContext _context;
    public MovieService(AppDbContext context) => _context = context;

    public async Task<(List<Movie> Items, int TotalCount)> GetMoviesAsync(string? keyword, int? genreId, int? releaseYear, int page, int pageSize)
    {
        var query = _context.Movies.Include(m => m.Genre).AsQueryable();

        // Lọc theo từ khóa
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(m => m.Title.Contains(keyword));

        // Lọc theo thể loại
        if (genreId.HasValue)
            query = query.Where(m => m.GenreId == genreId);

        // Lọc theo năm phát hành
        if (releaseYear.HasValue)
            query = query.Where(m => m.ReleaseYear == releaseYear);

        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Movie> GetMovieByIdAsync(int id) =>
        await _context.Movies.Include(m => m.Genre).Include(m => m.Episodes).FirstOrDefaultAsync(m => m.Id == id);

    // 1. Hàm AddMovieAsync (Sửa SaveChanges thành SaveChangesAsync)
    public async Task AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(); // <--- THÊM "Async" VÀO ĐÂY
    }

    // 2. Hàm UpdateMovieAsync (Bạn đã làm đúng rồi, nhưng kiểm tra lại nhé)
    public async Task UpdateMovieAsync(int id, Movie movieUpdate)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            movie.Title = movieUpdate.Title;
            movie.Poster = movieUpdate.Poster;
            movie.GenreId = movieUpdate.GenreId;
            await _context.SaveChangesAsync(); // <--- Dòng này ĐÚNG
        }
    }

    // 3. Hàm DeleteMovieAsync (Sửa SaveChanges thành SaveChangesAsync)
    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(); // <--- THÊM "Async" VÀO ĐÂY
        }
    }

    public async Task RateMovieAsync(int id, int ratingValue)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            var totalRatingScore = movie.AverageRating * movie.RatingCount;
            movie.RatingCount++;
            movie.AverageRating = (totalRatingScore + ratingValue) / movie.RatingCount;
            
            await _context.SaveChangesAsync();
        }
    }
}