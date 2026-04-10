using Microsoft.EntityFrameworkCore;
using Netflix.Data.Context;
using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) => _context = context;

    public async Task<List<User>> GetAllUsersAsync() =>
        await _context.Users.ToListAsync();

    public async Task<User> GetUserByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<bool> UpdateUserAsync(int id, User userUpdate)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        // Cập nhật các trường dựa trên file User.cs bạn vừa gửi
        user.Username = userUpdate.Username; // Đã đổi thành Username
        user.Email = userUpdate.Email;
        user.Role = userUpdate.Role;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeRoleAsync(int id, string newRole)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Role = newRole;
        await _context.SaveChangesAsync();
        return true;
    }
}