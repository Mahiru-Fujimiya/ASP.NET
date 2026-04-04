using Netflix.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Netflix.Business.Services;

public class MovieService : IMovieService
{
    private readonly AppDbContext _context;

    // Inject AppDbContext vào thông qua Constructor
    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    // Lấy danh sách phim thực tế từ bảng Movies
    public object GetMovies()
    {
        var movies = _context.Movies
                             .Include(m => m.Genre) // Lấy kèm thông tin Thể loại (Chương 5)
                             .ToList();
        return movies;
    }

    // Lấy chi tiết 1 bộ phim theo ID
    public object GetMovieById(int id)
    {
        var movie = _context.Movies
                            .Include(m => m.Genre)
                            .Include(m => m.Episodes) // Lấy kèm các tập phim
                            .FirstOrDefault(m => m.Id == id);
        return movie;
    }

    public object SearchMovies(string title)
    {
        // Tìm kiếm phim có tên chứa từ khóa (không phân biệt hoa thường)
        return _context.Movies
                       .Include(m => m.Genre)
                       .Where(m => m.Title.Contains(title))
                       .ToList();
    }
}