using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Business.Services;
using Netflix.API.Middlewares; // Chú ý namespace có chữ 's' nếu Boss đặt tên thư mục là Middlewares
using Netflix.Data.Seed;      // Đảm bảo namespace của MovieSeed đúng

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký CORS (Để Frontend gọi được API)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Đăng ký các dịch vụ xử lý logic (DI)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IStreamingService, StreamingService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// 🚀 PIPELINE CONFIGURATION (Thứ tự cực kỳ quan trọng)
// ============================================================

// A. Xử lý lỗi tập trung: Đưa ra ngoài IF để bảo vệ cả bản Production
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// B. Phục vụ Web tĩnh (index.html, admin.html)
// UseDefaultFiles phải nằm TRƯỚC UseStaticFiles
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

// C. CORS nằm sau Routing nhưng trước Auth
app.UseCors("AllowAll");

// D. Bảo mật (Thứ tự: AuthN -> AuthZ)
// app.UseAuthentication(); // Bật lên khi Boss cấu hình JWT
app.UseAuthorization();

app.MapControllers();

// ============================================================
// 🚀 TỰ ĐỘNG BƠM DỮ LIỆU (SEED DATA)
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Sử dụng await vì đây là tác vụ I/O database
        await MovieSeed.SeedAsync(context);
        Console.WriteLine("✅ Hệ thống LawwyFlix: Seed Data thành công!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Lỗi Seed Data: " + ex.Message);
    }
}

app.Run();