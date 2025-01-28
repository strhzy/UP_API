using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QualificationController : ControllerBase
{
    private readonly QualificationService _qualificationService;

    public QualificationController(QualificationService qualificationService)
    {
        _qualificationService = qualificationService;
    }

    // Получить все квалификации
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Qualification>>> GetAllQualifications()
    {
        var qualifications = await _qualificationService.GetAllQualificationsAsync();
        return Ok(qualifications);
    }

    // Получить квалификацию по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Qualification>> GetQualificationById(long id)
    {
        var qualification = await _qualificationService.GetQualificationByIdAsync(id);
        if (qualification == null)
        {
            return NotFound();
        }
        return Ok(qualification);
    }

    // Добавить новую квалификацию
    [HttpPost]
    public async Task<ActionResult<Qualification>> AddQualification(Qualification qualification)
    {
        var newQualification = await _qualificationService.AddQualificationAsync(qualification);
        return CreatedAtAction(nameof(GetQualificationById), new { id = newQualification.Id }, newQualification);
    }

    // Обновить квалификацию
    [HttpPut("{id}")]
    public async Task<ActionResult<Qualification>> UpdateQualification(long id, Qualification updatedQualification)
    {
        var qualification = await _qualificationService.UpdateQualificationAsync(id, updatedQualification);
        if (qualification == null)
        {
            return NotFound();
        }
        return Ok(qualification);
    }

    // Удалить квалификацию
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQualification(long id)
    {
        var result = await _qualificationService.DeleteQualificationAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}