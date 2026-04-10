using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities; // Boss check lại xem trong project Data thư mục chứa Movie.cs tên là gì (Entities hay Models)

namespace Netflix.Data.Seed
{
    public static class MovieSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // 1. Kiểm tra nếu có phim rồi thì thôi
            if (await context.Movies.AnyAsync()) return;

            // 2. Tạo Genre (Chỉ dùng cột Name vì chắc chắn nó tồn tại)
            var animeGenre = new Genre { Name = "Anime" };
            var actionGenre = new Genre { Name = "Action" };

            await context.Genres.AddRangeAsync(animeGenre, actionGenre);
            await context.SaveChangesAsync();

            // 3. Tạo Movie (Chỉ dùng Title, Poster, GenreId)
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Aquarion Logos",
                    Poster = "https://images.alphacoders.com/133/1338193.png",
                    GenreId = animeGenre.Id
                },
                new Movie
                {
                    Title = "Suzume no Tojimari",
                    Poster = "https://images5.alphacoders.com/123/1235654.jpg",
                    GenreId = animeGenre.Id
                }
            };

            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }
    }
}