using API_UP.Data;
using API_UP.Models;

namespace API_UP.Services;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class StatusService
{
    private readonly MyDbContext _context;

    public StatusService(MyDbContext context)
    {
        _context = context;
    }

    // Получить все статусы
    public List<Status> GetAllStatuses()
    {
        return _context.Statuses.ToList();
    }

    // Получить статус по ID
    public Status GetStatusById(int id)
    {
        return _context.Statuses.FirstOrDefault(s => s.Id == id);
    }

    // Добавить новый статус
    public void AddStatus(Status status)
    {
        _context.Statuses.Add(status);
        _context.SaveChanges();
    }

    // Обновить существующий статус
    public void UpdateStatus(int id, Status updatedStatus)
    {
        var status = _context.Statuses.FirstOrDefault(s => s.Id == id);
        if (status != null)
        {
            status.StatusName = updatedStatus.StatusName;
            _context.SaveChanges();
        }
    }

    // Удалить статус по ID
    public void DeleteStatus(int id)
    {
        var status = _context.Statuses.FirstOrDefault(s => s.Id == id);
        if (status != null)
        {
            _context.Statuses.Remove(status);
            _context.SaveChanges();
        }
    }
}