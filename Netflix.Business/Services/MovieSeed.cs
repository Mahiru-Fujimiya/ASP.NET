using Netflix.Data.Context;
using Netflix.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Netflix.Business.Services;

public static class MovieSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Kiểm tra nếu đã có phim rồi thì không chạy nữa để tránh trùng lặp
        if (await context.Movies.AnyAsync()) return;

        var movies = new List<Movie>
        {
            new Movie {
                Title = "Naruto: Shippuden",
                Poster = "https://image.tmdb.org/t/p/original/o9OKUmSqc0Zjq97QC0fJaSTC5nq.jpg",
                GenreId = 1 // Nhớ check ID này có trong bảng Genre chưa nhé Phúc
            },
            new Movie {
                Title = "Solo Leveling",
                Poster = "https://image.tmdb.org/t/p/original/geuS9TejRq6nnI7068vS8Z87m9p.jpg",
                GenreId = 1
            },
            new Movie {
                Title = "John Wick: Chapter 4",
                Poster = "https://image.tmdb.org/t/p/original/vZloYm7pS92aseOpiJy69UB47io.jpg",
                GenreId = 2
            },
            new Movie {
                Title = "Oppenheimer",
                Poster = "https://image.tmdb.org/t/p/original/8Gxv2mSjUjZ7S9S3TzD3CcU4uC.jpg",
                GenreId = 3
            },
            new Movie {
                Title = "Demon Slayer: Mugen Train",
                Poster = "https://image.tmdb.org/t/p/original/h8Yp9Pcy6N0ZcsS9pQ7oiFLXpYf.jpg",
                GenreId = 1
            }
        };

        await context.Movies.AddRangeAsync(movies);
        await context.SaveChangesAsync();
    }
}