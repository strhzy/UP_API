using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class ClientService
{
    private readonly MyDbContext _context;

    public ClientService(MyDbContext context)
    {
        _context = context;
    }

    // Получить всех клиентов
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    // Получить клиента по ID
    public async Task<Client?> GetClientByIdAsync(long id)
    {
        return await _context.Clients.FindAsync(id);
    }

    // Добавить нового клиента
    public async Task<Client> AddClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    // Обновить информацию о клиенте
    public async Task<Client?> UpdateClientAsync(long id, Client updatedClient)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return null;
        }

        client.Surname = updatedClient.Surname;
        client.ClientName = updatedClient.ClientName;
        client.Patronymic = updatedClient.Patronymic;
        client.TelephoneNumber = updatedClient.TelephoneNumber;
        client.CarBrand = updatedClient.CarBrand;
        client.CarModel = updatedClient.CarModel;
        client.GovNumber = updatedClient.GovNumber;
        client.LastVisitDate = updatedClient.LastVisitDate;

        await _context.SaveChangesAsync();
        return client;
    }

    // Удалить клиента
    public async Task<bool> DeleteClientAsync(long id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return false;
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}