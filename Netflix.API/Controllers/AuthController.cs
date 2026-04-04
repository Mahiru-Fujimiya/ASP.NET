using Microsoft.AspNetCore.Mvc;
using Netflix.Business.Services;
using Netflix.Business.DTOs; // CHỈ giữ lại dòng này

namespace Netflix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        return Ok(_authService.Register(request));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(_authService.Login(request));
    }
}