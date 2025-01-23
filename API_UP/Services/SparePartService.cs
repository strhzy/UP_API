using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class SparePartService
{
    private readonly MyDbContext _context;
    private readonly ILogger<SparePartService> _logger;

    public SparePartService(MyDbContext context, ILogger<SparePartService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все запчасти
    public async Task<IEnumerable<SparePart>> GetAllSparePartsAsync()
    {
        return await _context.SpareParts.ToListAsync();
    }

    // Получить запчасть по ID
    public async Task<SparePart?> GetSparePartByIdAsync(long id)
    {
        return await _context.SpareParts.FindAsync(id);
    }

    // Добавить новую запчасть
    public async Task<SparePart> AddSparePartAsync(SparePart sparePart)
    {
        _context.SpareParts.Add(sparePart);
        await _context.SaveChangesAsync();
        return sparePart;
    }

    // Обновить запчасть
    public async Task<SparePart?> UpdateSparePartAsync(long id, SparePart updatedSparePart)
    {
        var sparePart = await _context.SpareParts.FindAsync(id);
        if (sparePart == null)
        {
            return null;
        }

        sparePart.PartName = updatedSparePart.PartName;
        sparePart.Price = updatedSparePart.Price;

        await _context.SaveChangesAsync();
        return sparePart;
    }

    // Удалить запчасть
    public async Task<bool> DeleteSparePartAsync(long id)
    {
        var sparePart = await _context.SpareParts.FindAsync(id);
        if (sparePart == null)
        {
            return false;
        }

        _context.SpareParts.Remove(sparePart);
        await _context.SaveChangesAsync();
        return true;
    }
}