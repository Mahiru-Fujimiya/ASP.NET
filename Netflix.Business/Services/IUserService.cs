using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(int id, User userUpdate);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> ChangeRoleAsync(int id, string newRole);
}