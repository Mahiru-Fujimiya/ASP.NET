using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    public AuthService(AppDbContext context) => _context = context;

    public async Task<bool> RegisterAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
        if (user == null) return null;

        // Trả về một Token giả lập vì chưa cấu hình JWT hoàn chỉnh
        return "fake-jwt-token-for-" + user.Username;
    }
}