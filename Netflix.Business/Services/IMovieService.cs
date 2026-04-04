namespace Netflix.Business.Services;

public interface IMovieService
{
    object GetMovies();
    object GetMovieById(int id);
    object SearchMovies(string title); // Thêm dòng này vào
}