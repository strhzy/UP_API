using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceStationController : ControllerBase
{
    private readonly ServiceStationService _serviceStationService;

    public ServiceStationController(ServiceStationService serviceStationService)
    {
        _serviceStationService = serviceStationService;
    }

    // Получить все сервисные станции
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceStation>>> GetAllServiceStations()
    {
        var serviceStations = await _serviceStationService.GetAllServiceStationsAsync();
        return Ok(serviceStations);
    }

    // Получить сервисную станцию по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceStation>> GetServiceStationById(long id)
    {
        var serviceStation = await _serviceStationService.GetServiceStationByIdAsync(id);
        if (serviceStation == null)
        {
            return NotFound();
        }
        return Ok(serviceStation);
    }

    // Добавить новую сервисную станцию
    [HttpPost]
    public async Task<ActionResult<ServiceStation>> AddServiceStation(ServiceStation serviceStation)
    {
        var newServiceStation = await _serviceStationService.AddServiceStationAsync(serviceStation);
        return CreatedAtAction(nameof(GetServiceStationById), new { id = newServiceStation.Id }, newServiceStation);
    }

    // Обновить сервисную станцию
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceStation>> UpdateServiceStation(long id, ServiceStation updatedServiceStation)
    {
        var serviceStation = await _serviceStationService.UpdateServiceStationAsync(id, updatedServiceStation);
        if (serviceStation == null)
        {
            return NotFound();
        }
        return Ok(serviceStation);
    }

    // Удалить сервисную станцию
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceStation(long id)
    {
        var result = await _serviceStationService.DeleteServiceStationAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}