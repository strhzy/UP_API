using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class OrderService
{
    private readonly MyDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public OrderService(MyDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все заказы
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    // Получить заказ по ID
    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        return await _context.Orders.FindAsync(id);
    }

    // Добавить новый заказ
    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Обновить заказ
    public async Task<Order?> UpdateOrderAsync(long id, Order updatedOrder)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return null;
        }

        order.ClientId = updatedOrder.ClientId;
        order.DateReference = updatedOrder.DateReference;
        order.RepairDate = updatedOrder.RepairDate;
        order.StatusName = updatedOrder.StatusName;

        await _context.SaveChangesAsync();
        return order;
    }

    // Удалить заказ
    public async Task<bool> DeleteOrderAsync(long id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}