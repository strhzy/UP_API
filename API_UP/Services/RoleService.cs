using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class RoleService
{
    private readonly MyDbContext _context;
    private readonly ILogger<RoleService> _logger;

    public RoleService(MyDbContext context, ILogger<RoleService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все роли
    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    // Получить роль по ID
    public async Task<Role?> GetRoleByIdAsync(long id)
    {
        return await _context.Roles.FindAsync(id);
    }

    // Добавить новую роль
    public async Task<Role> AddRoleAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    // Обновить роль
    public async Task<Role?> UpdateRoleAsync(long id, Role updatedRole)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return null;
        }

        role.RoleName = updatedRole.RoleName;

        await _context.SaveChangesAsync();
        return role;
    }

    // Удалить роль
    public async Task<bool> DeleteRoleAsync(long id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
        {
            return false;
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }
}