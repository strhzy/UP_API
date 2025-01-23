using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class QualificationService
{
    private readonly MyDbContext _context;
    private readonly ILogger<QualificationService> _logger;

    public QualificationService(MyDbContext context, ILogger<QualificationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все квалификации
    public async Task<IEnumerable<Qualification>> GetAllQualificationsAsync()
    {
        return await _context.Qualifications.ToListAsync();
    }

    // Получить квалификацию по ID
    public async Task<Qualification?> GetQualificationByIdAsync(long id)
    {
        return await _context.Qualifications.FindAsync(id);
    }

    // Добавить новую квалификацию
    public async Task<Qualification> AddQualificationAsync(Qualification qualification)
    {
        _context.Qualifications.Add(qualification);
        await _context.SaveChangesAsync();
        return qualification;
    }

    // Обновить квалификацию
    public async Task<Qualification?> UpdateQualificationAsync(long id, Qualification updatedQualification)
    {
        var qualification = await _context.Qualifications.FindAsync(id);
        if (qualification == null)
        {
            return null;
        }

        qualification.QualificationName = updatedQualification.QualificationName;

        await _context.SaveChangesAsync();
        return qualification;
    }

    // Удалить квалификацию
    public async Task<bool> DeleteQualificationAsync(long id)
    {
        var qualification = await _context.Qualifications.FindAsync(id);
        if (qualification == null)
        {
            return false;
        }

        _context.Qualifications.Remove(qualification);
        await _context.SaveChangesAsync();
        return true;
    }
}