using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public interface IMovieService
{
    // Phải có Task ở đầu để dùng được await
    Task<(List<Movie> Items, int TotalCount)> GetMoviesAsync(string? keyword, int? genreId, int page, int pageSize);

    Task<Movie> GetMovieByIdAsync(int id);

    // Thay đổi từ void/int sang Task
    Task AddMovieAsync(Movie movie);

    Task UpdateMovieAsync(int id, Movie movieUpdate);

    Task DeleteMovieAsync(int id);
}