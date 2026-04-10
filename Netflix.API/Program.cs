using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Đăng ký các dịch vụ xử lý logic
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IStreamingService, StreamingService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// 🚀 ĐOẠN MỚI: TỰ ĐỘNG BƠM DỮ LIỆU (SEED DATA)
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Gọi hàm Seed từ file MovieSeed.cs bạn vừa tạo
        await MovieSeed.SeedAsync(context);
    }
    catch (Exception ex)
    {
        // Nếu có lỗi khi bơm dữ liệu thì hiện ở đây
        Console.WriteLine("Lỗi Seed Data: " + ex.Message);
    }
}
// ============================================================

// 4. Cấu hình Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();