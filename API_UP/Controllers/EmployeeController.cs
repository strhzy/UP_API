using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // Получить всех сотрудников
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    // Получить сотрудника по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(long id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // Добавить нового сотрудника
    [HttpPost]
    public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
    {
        var newEmployee = await _employeeService.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
    }

    // Обновить информацию о сотруднике
    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(long id, Employee updatedEmployee)
    {
        var employee = await _employeeService.UpdateEmployeeAsync(id, updatedEmployee);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // Удалить сотрудника
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(long id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}