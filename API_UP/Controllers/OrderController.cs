using API_UP.Models;
using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    // Получить все заказы
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // Получить заказ по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(long id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    // Добавить новый заказ
    [HttpPost]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        var newOrder = await _orderService.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
    }

    // Обновить заказ
    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(long id, Order updatedOrder)
    {
        var order = await _orderService.UpdateOrderAsync(id, updatedOrder);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    // Удалить заказ
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        var result = await _orderService.DeleteOrderAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}