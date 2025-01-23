using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API_UP.Services;

public class AuthService
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(MyDbContext context, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    // Аутентификация пользователя
    public async Task<string?> AuthenticateAsync(string login, string password)
    {
        var employeeAccount = await _context.EmployeeAccounts
            .Include(ea => ea.Role)
            .FirstOrDefaultAsync(ea => ea.Login == login);

        if (employeeAccount == null || !BCrypt.Net.BCrypt.Verify(password, employeeAccount.Password))
        {
            return null; // Неверный логин или пароль
        }

        // Генерация JWT-токена
        var token = GenerateJwtToken(employeeAccount);
        return token;
    }

    // Генерация JWT-токена
    private string GenerateJwtToken(EmployeeAccount employeeAccount)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, employeeAccount.Id.ToString()),
            new Claim(ClaimTypes.Name, employeeAccount.Login),
            new Claim(ClaimTypes.Role, employeeAccount.Role.RoleName)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Регистрация нового пользователя
    public async Task<EmployeeAccount> RegisterAsync(EmployeeAccount employeeAccount)
    {
        // Хеширование пароля
        employeeAccount.Password = BCrypt.Net.BCrypt.HashPassword(employeeAccount.Password);

        _context.EmployeeAccounts.Add(employeeAccount);
        await _context.SaveChangesAsync();
        return employeeAccount;
    }
}