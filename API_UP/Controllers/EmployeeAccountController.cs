using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeAccountController : ControllerBase
{
    private readonly EmployeeAccountService _employeeAccountService;

    public EmployeeAccountController(EmployeeAccountService employeeAccountService)
    {
        _employeeAccountService = employeeAccountService;
    }

    // Получить все учетные записи
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeAccount>>> GetAllEmployeeAccounts()
    {
        var employeeAccounts = await _employeeAccountService.GetAllEmployeeAccountsAsync();
        return Ok(employeeAccounts);
    }

    // Получить учетную запись по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeAccount>> GetEmployeeAccountById(long id)
    {
        var employeeAccount = await _employeeAccountService.GetEmployeeAccountByIdAsync(id);
        if (employeeAccount == null)
        {
            return NotFound();
        }
        return Ok(employeeAccount);
    }

    // Добавить новую учетную запись
    [HttpPost]
    public async Task<ActionResult<EmployeeAccount>> AddEmployeeAccount(EmployeeAccount employeeAccount)
    {
        var newEmployeeAccount = await _employeeAccountService.AddEmployeeAccountAsync(employeeAccount);
        return CreatedAtAction(nameof(GetEmployeeAccountById), new { id = newEmployeeAccount.Id }, newEmployeeAccount);
    }

    // Обновить учетную запись
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeAccount>> UpdateEmployeeAccount(long id, EmployeeAccount updatedEmployeeAccount)
    {
        var employeeAccount = await _employeeAccountService.UpdateEmployeeAccountAsync(id, updatedEmployeeAccount);
        if (employeeAccount == null)
        {
            return NotFound();
        }
        return Ok(employeeAccount);
    }

    // Удалить учетную запись
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAccount(long id)
    {
        var result = await _employeeAccountService.DeleteEmployeeAccountAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}