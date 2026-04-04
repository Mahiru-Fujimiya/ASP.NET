using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Database (Chương 5)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các dịch vụ xử lý logic (Chương 11)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IStreamingService, StreamingService>();
builder.Services.AddScoped<IWatchHistoryService, WatchHistoryService>();

builder.Services.AddControllers();

// Thêm hỗ trợ Swagger để kiểm thử API (Chương 11)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình Pipeline (Chương 7)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();