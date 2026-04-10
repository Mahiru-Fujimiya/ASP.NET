using System.Net;
using System.Text.Json;

namespace Netflix.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Boss ơi! Có biến tại: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = "Hệ thống LawwyFlix đang bảo trì hoặc có lỗi nhỏ. Boss kiểm tra log nhé!",
            detail = exception.Message // Chỉ hiện detail khi debug, khi publish nên ẩn đi
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}