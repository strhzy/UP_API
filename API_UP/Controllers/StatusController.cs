using API_UP.Data;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

using System.Collections.Generic;


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly StatusService _statusService;

    public StatusController(StatusService statusService)
    {
        _statusService = statusService;
    }

    // GET: api/Status
    [HttpGet]
    public ActionResult<List<Status>> GetAllStatuses()
    {
        return _statusService.GetAllStatuses();
    }

    // GET: api/Status/5
    [HttpGet("{id}")]
    public ActionResult<Status> GetStatus(int id)
    {
        var status = _statusService.GetStatusById(id);
        if (status == null)
        {
            return NotFound();
        }
        return status;
    }

    // POST: api/Status
    [HttpPost]
    public ActionResult<Status> AddStatus(Status status)
    {
        _statusService.AddStatus(status);
        return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
    }

    // PUT: api/Status/5
    [HttpPut("{id}")]
    public IActionResult UpdateStatus(int id, Status updatedStatus)
    {
        var status = _statusService.GetStatusById(id);
        if (status == null)
        {
            return NotFound();
        }

        _statusService.UpdateStatus(id, updatedStatus);
        return NoContent();
    }

    // DELETE: api/Status/5
    [HttpDelete("{id}")]
    public IActionResult DeleteStatus(int id)
    {
        var status = _statusService.GetStatusById(id);
        if (status == null)
        {
            return NotFound();
        }

        _statusService.DeleteStatus(id);
        return NoContent();
    }
}