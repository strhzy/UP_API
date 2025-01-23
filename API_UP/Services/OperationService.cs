using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class OperationService
{
    private readonly MyDbContext _context;
    private readonly ILogger<OperationService> _logger;

    public OperationService(MyDbContext context, ILogger<OperationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все операции
    public async Task<IEnumerable<Operation>> GetAllOperationsAsync()
    {
        return await _context.Operations.ToListAsync();
    }

    // Получить операцию по ID
    public async Task<Operation?> GetOperationByIdAsync(long id)
    {
        return await _context.Operations.FindAsync(id);
    }

    // Добавить новую операцию
    public async Task<Operation> AddOperationAsync(Operation operation)
    {
        _context.Operations.Add(operation);
        await _context.SaveChangesAsync();
        return operation;
    }

    // Обновить операцию
    public async Task<Operation?> UpdateOperationAsync(long id, Operation updatedOperation)
    {
        var operation = await _context.Operations.FindAsync(id);
        if (operation == null)
        {
            return null;
        }

        operation.OperationName = updatedOperation.OperationName;
        operation.Price = updatedOperation.Price;

        await _context.SaveChangesAsync();
        return operation;
    }

    // Удалить операцию
    public async Task<bool> DeleteOperationAsync(long id)
    {
        var operation = await _context.Operations.FindAsync(id);
        if (operation == null)
        {
            return false;
        }

        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
        return true;
    }
}