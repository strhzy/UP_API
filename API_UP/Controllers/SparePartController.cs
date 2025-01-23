using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SparePartController : ControllerBase
{
    private readonly SparePartService _sparePartService;

    public SparePartController(SparePartService sparePartService)
    {
        _sparePartService = sparePartService;
    }

    // Получить все запчасти
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SparePart>>> GetAllSpareParts()
    {
        var spareParts = await _sparePartService.GetAllSparePartsAsync();
        return Ok(spareParts);
    }

    // Получить запчасть по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<SparePart>> GetSparePartById(long id)
    {
        var sparePart = await _sparePartService.GetSparePartByIdAsync(id);
        if (sparePart == null)
        {
            return NotFound();
        }
        return Ok(sparePart);
    }

    // Добавить новую запчасть
    [HttpPost]
    public async Task<ActionResult<SparePart>> AddSparePart(SparePart sparePart)
    {
        var newSparePart = await _sparePartService.AddSparePartAsync(sparePart);
        return CreatedAtAction(nameof(GetSparePartById), new { id = newSparePart.Id }, newSparePart);
    }

    // Обновить запчасть
    [HttpPut("{id}")]
    public async Task<ActionResult<SparePart>> UpdateSparePart(long id, SparePart updatedSparePart)
    {
        var sparePart = await _sparePartService.UpdateSparePartAsync(id, updatedSparePart);
        if (sparePart == null)
        {
            return NotFound();
        }
        return Ok(sparePart);
    }

    // Удалить запчасть
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSparePart(long id)
    {
        var result = await _sparePartService.DeleteSparePartAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}