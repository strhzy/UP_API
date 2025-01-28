using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    // Получить все роли
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    // Получить роль по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRoleById(long id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    // Добавить новую роль
    [HttpPost]
    public async Task<ActionResult<Role>> AddRole(Role role)
    {
        var newRole = await _roleService.AddRoleAsync(role);
        return CreatedAtAction(nameof(GetRoleById), new { id = newRole.Id }, newRole);
    }

    // Обновить роль
    [HttpPut("{id}")]
    public async Task<ActionResult<Role>> UpdateRole(long id, Role updatedRole)
    {
        var role = await _roleService.UpdateRoleAsync(id, updatedRole);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    // Удалить роль
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(long id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}