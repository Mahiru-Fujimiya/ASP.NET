using Netflix.Data.Entities;

namespace Netflix.Business.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(User user);
    Task<string?> LoginAsync(string username, string password);
}