using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_UP.Services;

public class EmployeeAccountService
{
    private readonly MyDbContext _context;
    private readonly ILogger<EmployeeAccountService> _logger;

    public EmployeeAccountService(MyDbContext context, ILogger<EmployeeAccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все учетные записи
    public async Task<IEnumerable<EmployeeAccount>> GetAllEmployeeAccountsAsync()
    {
        return await _context.EmployeeAccounts.ToListAsync();
    }

    // Получить учетную запись по ID
    public async Task<EmployeeAccount?> GetEmployeeAccountByIdAsync(long id)
    {
        return await _context.EmployeeAccounts.FindAsync(id);
    }

    // Добавить новую учетную запись
    public async Task<EmployeeAccount> AddEmployeeAccountAsync(EmployeeAccount employeeAccount)
    {
        _context.EmployeeAccounts.Add(employeeAccount);
        await _context.SaveChangesAsync();
        return employeeAccount;
    }

    // Обновить учетную запись
    public async Task<EmployeeAccount?> UpdateEmployeeAccountAsync(long id, EmployeeAccount updatedEmployeeAccount)
    {
        var employeeAccount = await _context.EmployeeAccounts.FindAsync(id);
        if (employeeAccount == null)
        {
            return null;
        }

        employeeAccount.Login = updatedEmployeeAccount.Login;
        employeeAccount.Password = updatedEmployeeAccount.Password;
        employeeAccount.Telephone = updatedEmployeeAccount.Telephone;
        employeeAccount.Email = updatedEmployeeAccount.Email;
        employeeAccount.RoleId = updatedEmployeeAccount.RoleId;

        await _context.SaveChangesAsync();
        return employeeAccount;
    }

    // Удалить учетную запись
    public async Task<bool> DeleteEmployeeAccountAsync(long id)
    {
        var employeeAccount = await _context.EmployeeAccounts.FindAsync(id);
        if (employeeAccount == null)
        {
            return false;
        }

        _context.EmployeeAccounts.Remove(employeeAccount);
        await _context.SaveChangesAsync();
        return true;
    }
}