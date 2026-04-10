using Netflix.Data.Context;
using Netflix.Data.Entities;
using Netflix.Business.DTOs; // Sửa thành Business

namespace Netflix.Business.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    public AuthService(AppDbContext context) => _context = context;

    public object Register(RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
            return new { message = "Email đã tồn tại" };

        var user = new User { Username = request.Username, Email = request.Email, PasswordHash = request.Password };
        _context.Users.Add(user);
        _context.SaveChanges();
        return new { message = "Đăng ký thành công" };
    }

    public object Login(LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.PasswordHash == request.Password);
        if (user == null) return new { message = "Sai tài khoản hoặc mật khẩu" };
        return new { message = "Đăng nhập thành công", username = user.Username };
    }
}