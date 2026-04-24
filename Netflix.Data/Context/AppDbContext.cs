using Microsoft.EntityFrameworkCore; // [cite: 217]
using Netflix.Data.Entities;         // Quan trọng: Để tìm thấy User, Movie, Genre...

namespace Netflix.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
    }
}