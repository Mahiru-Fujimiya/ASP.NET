namespace Netflix.Business.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

namespace Netflix.Business.Services
{
    using Netflix.Business.DTOs;

    public interface IAuthService
    {
        object Register(RegisterRequest request);
        object Login(LoginRequest request);
    }
}