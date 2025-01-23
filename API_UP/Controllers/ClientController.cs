using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    // Получить всех клиентов
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    // Получить клиента по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientById(long id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    // Добавить нового клиента
    [HttpPost]
    public async Task<ActionResult<Client>> AddClient(Client client)
    {
        var newClient = await _clientService.AddClientAsync(client);
        return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
    }

    // Обновить информацию о клиенте
    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> UpdateClient(long id, Client updatedClient)
    {
        var client = await _clientService.UpdateClientAsync(id, updatedClient);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    // Удалить клиента
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(long id)
    {
        var result = await _clientService.DeleteClientAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}