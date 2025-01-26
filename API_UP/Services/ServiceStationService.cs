using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class ServiceStationService
{
    private readonly MyDbContext _context;
    private readonly ILogger<ServiceStationService> _logger;

    public ServiceStationService(MyDbContext context, ILogger<ServiceStationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все сервисные станции
    public async Task<IEnumerable<ServiceStation>> GetAllServiceStationsAsync()
    {
        return await _context.ServiceStations.ToListAsync();
    }

    // Получить сервисную станцию по ID
    public async Task<ServiceStation?> GetServiceStationByIdAsync(long id)
    {
        return await _context.ServiceStations.FindAsync(id);
    }

    // Добавить новую сервисную станцию
    public async Task<ServiceStation> AddServiceStationAsync(ServiceStation serviceStation)
    {
        _context.ServiceStations.Add(serviceStation);
        await _context.SaveChangesAsync();
        return serviceStation;
    }

    // Обновить сервисную станцию
    public async Task<ServiceStation?> UpdateServiceStationAsync(long id, ServiceStation updatedServiceStation)
    {
        var serviceStation = await _context.ServiceStations.FindAsync(id);
        if (serviceStation == null)
        {
            return null;
        }

        serviceStation.Address = updatedServiceStation.Address;
        serviceStation.TelephoneNumber = updatedServiceStation.TelephoneNumber;
        serviceStation.Email = updatedServiceStation.Email;
        serviceStation.QuantityWorkPlaces = updatedServiceStation.QuantityWorkPlaces;

        await _context.SaveChangesAsync();
        return serviceStation;
    }

    // Удалить сервисную станцию
    public async Task<bool> DeleteServiceStationAsync(long id)
    {
        var serviceStation = await _context.ServiceStations.FindAsync(id);
        if (serviceStation == null)
        {
            return false;
        }

        _context.ServiceStations.Remove(serviceStation);
        await _context.SaveChangesAsync();
        return true;
    }
}