using API_UP.Models;
using Microsoft.AspNetCore.Mvc;
using API_UP.Services;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderSparePartController : ControllerBase
{
    private readonly OrderSparePartService _orderSparePartService;

    public OrderSparePartController(OrderSparePartService orderSparePartService)
    {
        _orderSparePartService = orderSparePartService;
    }

    // Получить все связи между заказами и запчастями
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderSparePart>>> GetAllOrderSparePart()
    {
        var orderSparePart = await _orderSparePartService.GetAllOrderSparePartAsync();
        return Ok(orderSparePart);
    }

    // Получить связь по ID заказа и ID запчасти
    [HttpGet("{orderId}/{sparePartId}")]
    public async Task<ActionResult<OrderSparePart>> GetOrderSparePartById(long orderId, long sparePartId)
    {
        var orderSparePart = await _orderSparePartService.GetOrderSparePartByIdAsync(orderId, sparePartId);
        if (orderSparePart == null)
        {
            return NotFound();
        }
        return Ok(orderSparePart);
    }

    // Добавить новую связь между заказом и запчастью
    [HttpPost]
    public async Task<ActionResult<OrderSparePart>> AddOrderSparePart(OrderSparePart orderSparePart)
    {
        var newOrderSparePart = await _orderSparePartService.AddOrderSparePartAsync(orderSparePart);
        return CreatedAtAction(nameof(GetOrderSparePartById), new { orderId = newOrderSparePart.OrderId, sparePartId = newOrderSparePart.SparePartId }, newOrderSparePart);
    }

    // Обновить связь между заказом и запчастью
    [HttpPut("{orderId}/{sparePartId}")]
    public async Task<ActionResult<OrderSparePart>> UpdateOrderSparePart(long orderId, long sparePartId, OrderSparePart updatedOrderSparePart)
    {
        var orderSparePart = await _orderSparePartService.UpdateOrderSparePartAsync(orderId, sparePartId, updatedOrderSparePart);
        if (orderSparePart == null)
        {
            return NotFound();
        }
        return Ok(orderSparePart);
    }

    // Удалить связь между заказом и запчастью
    [HttpDelete("{orderId}/{sparePartId}")]
    public async Task<IActionResult> DeleteOrderSparePart(long orderId, long sparePartId)
    {
        var result = await _orderSparePartService.DeleteOrderSparePartAsync(orderId, sparePartId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}