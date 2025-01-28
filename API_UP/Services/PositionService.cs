using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API_UP.Services;

public class PositionService
{
    private readonly MyDbContext _context;
    private readonly ILogger<PositionService> _logger;

    public PositionService(MyDbContext context, ILogger<PositionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все должности
    public async Task<IEnumerable<Position>> GetAllPositionsAsync()
    {
        return await _context.Positions.ToListAsync();
    }

    // Получить должность по ID
    public async Task<Position?> GetPositionByIdAsync(long id)
    {
        return await _context.Positions.FindAsync(id);
    }

    // Добавить новую должность
    public async Task<Position> AddPositionAsync(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
        return position;
    }

    // Обновить должность
    public async Task<Position?> UpdatePositionAsync(long id, Position updatedPosition)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null)
        {
            return null;
        }

        position.PositionName = updatedPosition.PositionName;

        await _context.SaveChangesAsync();
        return position;
    }

    // Удалить должность
    public async Task<bool> DeletePositionAsync(long id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null)
        {
            return false;
        }

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return true;
    }
}