using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_UP.Services;

public class EmployeeService
{
    private readonly MyDbContext _context;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(MyDbContext context, ILogger<EmployeeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить всех сотрудников
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    // Получить сотрудника по ID
    public async Task<Employee?> GetEmployeeByIdAsync(long id)
    {
        return await _context.Employees.FindAsync(id);
    }

    // Добавить нового сотрудника
    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    // Обновить информацию о сотруднике
    public async Task<Employee?> UpdateEmployeeAsync(long id, Employee updatedEmployee)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return null;
        }

        employee.Surname = updatedEmployee.Surname;
        employee.EmployeeName = updatedEmployee.EmployeeName;
        employee.Patronymic = updatedEmployee.Patronymic;
        employee.PositionId = updatedEmployee.PositionId;
        employee.QualificationId = updatedEmployee.QualificationId;
        employee.ServiceStationId = updatedEmployee.ServiceStationId;
        employee.EmployeeAccountId = updatedEmployee.EmployeeAccountId;

        await _context.SaveChangesAsync();
        return employee;
    }

    // Удалить сотрудника
    public async Task<bool> DeleteEmployeeAsync(long id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}