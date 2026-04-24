using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Data.Seed
{
    public static class MovieSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Xóa dữ liệu cũ nếu muốn làm mới (Cảnh báo: Chỉ dùng cho Dev)
            if (await context.Movies.CountAsync() > 5) return;

            // Nếu DB chưa có nhiều phim, ta sẽ xóa trắng và tạo lại bộ seed mới 25 phim
            context.Movies.RemoveRange(context.Movies);
            context.Genres.RemoveRange(context.Genres);
            await context.SaveChangesAsync();

            // 1. Tạo 5 thể loại
            var animeGenre = new Genre { Name = "Anime", Description = "Phim hoạt hình Nhật Bản." };
            var actionGenre = new Genre { Name = "Action", Description = "Phim hành động kịch tính." };
            var scifiGenre = new Genre { Name = "Sci-Fi", Description = "Phim khoa học viễn tưởng." };
            var comedyGenre = new Genre { Name = "Comedy", Description = "Phim hài hước giải trí." };
            var horrorGenre = new Genre { Name = "Horror", Description = "Phim kinh dị giật gân." };

            await context.Genres.AddRangeAsync(animeGenre, actionGenre, scifiGenre, comedyGenre, horrorGenre);
            await context.SaveChangesAsync();

            // Hàm hỗ trợ tạo link poster tự động dựa trên tên phim
            string GetPoster(string title) => $"https://image.pollinations.ai/prompt/movie%20poster%20{Uri.EscapeDataString(title)}?width=400&height=600&nologo=true";

            // 2. Tạo 25 Movie (5 phim mỗi thể loại)
            var movies = new List<Movie>
            {
                // ANIME
                new Movie { Title = "Spirited Away", Poster = GetPoster("Spirited Away anime"), GenreId = animeGenre.Id, Description = "Cô bé Chihiro lạc vào thế giới linh hồn.", ReleaseYear = 2001 },
                new Movie { Title = "Your Name", Poster = GetPoster("Your Name anime"), GenreId = animeGenre.Id, Description = "Hai người xa lạ hoán đổi thể xác cho nhau.", ReleaseYear = 2016 },
                new Movie { Title = "Akira", Poster = GetPoster("Akira 1988 anime"), GenreId = animeGenre.Id, Description = "Dự án chính phủ tuyệt mật đe dọa Neo-Tokyo.", ReleaseYear = 1988 },
                new Movie { Title = "Princess Mononoke", Poster = GetPoster("Princess Mononoke anime"), GenreId = animeGenre.Id, Description = "Cuộc chiến giữa con người và thần rừng.", ReleaseYear = 1997 },
                new Movie { Title = "My Neighbor Totoro", Poster = GetPoster("My Neighbor Totoro anime"), GenreId = animeGenre.Id, Description = "Câu chuyện về thần rừng Totoro đáng yêu.", ReleaseYear = 1988 },

                // ACTION
                new Movie { Title = "Die Hard", Poster = GetPoster("Die Hard action movie"), GenreId = actionGenre.Id, Description = "Viên cảnh sát NY đối đầu bọn khủng bố.", ReleaseYear = 1988 },
                new Movie { Title = "John Wick", Poster = GetPoster("John Wick movie"), GenreId = actionGenre.Id, Description = "Sát thủ huyền thoại tái xuất giang hồ.", ReleaseYear = 2014 },
                new Movie { Title = "Mad Max: Fury Road", Poster = GetPoster("Mad Max Fury Road"), GenreId = actionGenre.Id, Description = "Cuộc đua sinh tử giữa sa mạc cằn cỗi.", ReleaseYear = 2015 },
                new Movie { Title = "The Dark Knight", Poster = GetPoster("The Dark Knight Batman"), GenreId = actionGenre.Id, Description = "Batman đối đầu với ác nhân Joker.", ReleaseYear = 2008 },
                new Movie { Title = "Gladiator", Poster = GetPoster("Gladiator 2000 movie"), GenreId = actionGenre.Id, Description = "Tướng quân La Mã trở thành đấu sĩ.", ReleaseYear = 2000 },

                // SCI-FI
                new Movie { Title = "Interstellar", Poster = GetPoster("Interstellar movie"), GenreId = scifiGenre.Id, Description = "Du hành không gian cứu lấy nhân loại.", ReleaseYear = 2014 },
                new Movie { Title = "Inception", Poster = GetPoster("Inception movie"), GenreId = scifiGenre.Id, Description = "Đánh cắp giấc mơ của mục tiêu.", ReleaseYear = 2010 },
                new Movie { Title = "The Matrix", Poster = GetPoster("The Matrix movie"), GenreId = scifiGenre.Id, Description = "Thế giới thực chất chỉ là một ma trận.", ReleaseYear = 1999 },
                new Movie { Title = "Blade Runner 2049", Poster = GetPoster("Blade Runner 2049"), GenreId = scifiGenre.Id, Description = "Người nhân bản khám phá bí mật khủng khiếp.", ReleaseYear = 2017 },
                new Movie { Title = "Avatar", Poster = GetPoster("Avatar Pandora movie"), GenreId = scifiGenre.Id, Description = "Cuộc chiến bảo vệ hành tinh Pandora.", ReleaseYear = 2009 },

                // COMEDY
                new Movie { Title = "The Hangover", Poster = GetPoster("The Hangover comedy"), GenreId = comedyGenre.Id, Description = "Bữa tiệc độc thân thảm họa ở Vegas.", ReleaseYear = 2009 },
                new Movie { Title = "Superbad", Poster = GetPoster("Superbad comedy"), GenreId = comedyGenre.Id, Description = "Hai cậu bạn tuổi teen kiếm rượu đi tiệc.", ReleaseYear = 2007 },
                new Movie { Title = "Step Brothers", Poster = GetPoster("Step Brothers comedy"), GenreId = comedyGenre.Id, Description = "Hai ông chú lớn tuổi vẫn ăn bám gia đình.", ReleaseYear = 2008 },
                new Movie { Title = "Dumb and Dumber", Poster = GetPoster("Dumb and Dumber comedy"), GenreId = comedyGenre.Id, Description = "Hai anh chàng ngốc nghếch đi trả lại vali.", ReleaseYear = 1994 },
                new Movie { Title = "21 Jump Street", Poster = GetPoster("21 Jump Street comedy"), GenreId = comedyGenre.Id, Description = "Hai cảnh sát chìm thâm nhập trường cấp 3.", ReleaseYear = 2012 },

                // HORROR
                new Movie { Title = "The Conjuring", Poster = GetPoster("The Conjuring horror"), GenreId = horrorGenre.Id, Description = "Vợ chồng pháp sư diệt trừ tà ma.", ReleaseYear = 2013 },
                new Movie { Title = "It", Poster = GetPoster("It Pennywise horror"), GenreId = horrorGenre.Id, Description = "Gã hề đáng sợ Pennywise đe dọa trẻ em.", ReleaseYear = 2017 },
                new Movie { Title = "Get Out", Poster = GetPoster("Get Out 2017 horror"), GenreId = horrorGenre.Id, Description = "Chuyến thăm gia đình bạn gái đầy bí ẩn.", ReleaseYear = 2017 },
                new Movie { Title = "A Nightmare on Elm Street", Poster = GetPoster("A Nightmare on Elm Street"), GenreId = horrorGenre.Id, Description = "Tên sát nhân tấn công nạn nhân trong giấc mơ.", ReleaseYear = 1984 },
                new Movie { Title = "Hereditary", Poster = GetPoster("Hereditary horror movie"), GenreId = horrorGenre.Id, Description = "Gia đình đối mặt với lời nguyền đen tối.", ReleaseYear = 2018 }
            };

            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }
    }
}