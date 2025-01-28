using System.Collections.Generic;
using System.Threading.Tasks;
using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly OperationService _operationService;

    public OperationController(OperationService operationService)
    {
        _operationService = operationService;
    }

    // Получить все операции
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Operation>>> GetAllOperations()
    {
        var operations = await _operationService.GetAllOperationsAsync();
        return Ok(operations);
    }

    // Получить операцию по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Operation>> GetOperationById(long id)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);
        if (operation == null)
        {
            return NotFound();
        }
        return Ok(operation);
    }

    // Добавить новую операцию
    [HttpPost]
    public async Task<ActionResult<Operation>> AddOperation(Operation operation)
    {
        var newOperation = await _operationService.AddOperationAsync(operation);
        return CreatedAtAction(nameof(GetOperationById), new { id = newOperation.Id }, newOperation);
    }

    // Обновить операцию
    [HttpPut("{id}")]
    public async Task<ActionResult<Operation>> UpdateOperation(long id, Operation updatedOperation)
    {
        var operation = await _operationService.UpdateOperationAsync(id, updatedOperation);
        if (operation == null)
        {
            return NotFound();
        }
        return Ok(operation);
    }

    // Удалить операцию
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOperation(long id)
    {
        var result = await _operationService.DeleteOperationAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}