using API_UP.Data;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Services;

public class OrderSparePartService
{
    private readonly MyDbContext _context;
    private readonly ILogger<OrderSparePartService> _logger;

    public OrderSparePartService(MyDbContext context, ILogger<OrderSparePartService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Получить все связи между заказами и запчастями
    public async Task<IEnumerable<OrderSparePart>> GetAllOrderSparePartAsync()
    {
        return await _context.OrderSpareParts
            .Include(osp => osp.Order)
            .Include(osp => osp.SparePart)
            .ToListAsync();
    }

    // Получить связь по ID заказа и ID запчасти
    public async Task<OrderSparePart?> GetOrderSparePartByIdAsync(long orderId, long sparePartId)
    {
        return await _context.OrderSpareParts
            .Include(osp => osp.Order)
            .Include(osp => osp.SparePart)
            .FirstOrDefaultAsync(osp => osp.OrderId == orderId && osp.SparePartId == sparePartId);
    }

    // Добавить новую связь между заказом и запчастью
    public async Task<OrderSparePart> AddOrderSparePartAsync(OrderSparePart orderSparePart)
    {
        _context.OrderSpareParts.Add(orderSparePart);
        await _context.SaveChangesAsync();
        return orderSparePart;
    }

    // Обновить связь между заказом и запчастью
    public async Task<OrderSparePart?> UpdateOrderSparePartAsync(long orderId, long sparePartId, OrderSparePart updatedOrderSparePart)
    {
        var orderSparePart = await _context.OrderSpareParts
            .FirstOrDefaultAsync(osp => osp.OrderId == orderId && osp.SparePartId == sparePartId);

        if (orderSparePart == null)
        {
            return null;
        }

        await _context.SaveChangesAsync();
        return orderSparePart;
    }

    // Удалить связь между заказом и запчастью
    public async Task<bool> DeleteOrderSparePartAsync(long orderId, long sparePartId)
    {
        var orderSparePart = await _context.OrderSpareParts
            .FirstOrDefaultAsync(osp => osp.OrderId == orderId && osp.SparePartId == sparePartId);

        if (orderSparePart == null)
        {
            return false;
        }

        _context.OrderSpareParts.Remove(orderSparePart);
        await _context.SaveChangesAsync();
        return true;
    }
}