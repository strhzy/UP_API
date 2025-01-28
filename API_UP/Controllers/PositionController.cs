using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly PositionService _positionService;

    public PositionController(PositionService positionService)
    {
        _positionService = positionService;
    }

    // Получить все должности
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions()
    {
        var positions = await _positionService.GetAllPositionsAsync();
        return Ok(positions);
    }

    // Получить должность по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Position>> GetPositionById(long id)
    {
        var position = await _positionService.GetPositionByIdAsync(id);
        if (position == null)
        {
            return NotFound();
        }
        return Ok(position);
    }

    // Добавить новую должность
    [HttpPost]
    public async Task<ActionResult<Position>> AddPosition(Position position)
    {
        var newPosition = await _positionService.AddPositionAsync(position);
        return CreatedAtAction(nameof(GetPositionById), new { id = newPosition.Id }, newPosition);
    }

    // Обновить должность
    [HttpPut("{id}")]
    public async Task<ActionResult<Position>> UpdatePosition(long id, Position updatedPosition)
    {
        var position = await _positionService.UpdatePositionAsync(id, updatedPosition);
        if (position == null)
        {
            return NotFound();
        }
        return Ok(position);
    }

    // Удалить должность
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePosition(long id)
    {
        var result = await _positionService.DeletePositionAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}