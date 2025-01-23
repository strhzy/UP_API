using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = API_UP.Models.LoginRequest;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // Аутентификация
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Login, request.Password);

        if (token == null)
        {
            return Unauthorized(); // Неверный логин или пароль
        }

        return Ok(new { Token = token });
    }

    // Регистрация
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] EmployeeAccount employeeAccount)
    {
        var newEmployeeAccount = await _authService.RegisterAsync(employeeAccount);
        return CreatedAtAction(nameof(Login), new { id = newEmployeeAccount.Id }, newEmployeeAccount);
    }
}