using API_UP.Data;

namespace API_UP.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using OfficeOpenXml;
using System.IO;

public interface IReportService
{
    byte[] GenerateIncomeReportExcel(DateTime startDate, DateTime endDate);
    byte[] GenerateSparePartsStockReportExcel();
    byte[] GeneratePopularOperationsReportExcel();
    byte[] GeneratePopularSparePartsReportExcel();
}

public class ReportService : IReportService
{
    private readonly MyDbContext _context;

    public ReportService(MyDbContext context)
    {
        _context = context;
    }

    // Отчет по доходам
    public byte[] GenerateIncomeReportExcel(DateTime startDate, DateTime endDate)
    {
        var incomeData = _context.Orders
            .Where(o => o.RepairDate >= startDate && o.RepairDate <= endDate)
            .Select(o => new
            {
                o.Id,
                o.Client.ClientName,
                o.RepairDate,
                o.Price
            })
            .ToList();

        // Рассчитываем общую сумму всех заказов
        decimal totalSum = incomeData.Sum(o => o.Price);

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Income Report");

            // Заголовки столбцов
            worksheet.Cells[1, 1].Value = "Order ID";
            worksheet.Cells[1, 2].Value = "Client Name";
            worksheet.Cells[1, 3].Value = "Repair Date";
            worksheet.Cells[1, 4].Value = "Price";

            // Данные
            for (int i = 0; i < incomeData.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = incomeData[i].Id;
                worksheet.Cells[i + 2, 2].Value = incomeData[i].ClientName;
                worksheet.Cells[i + 2, 3].Value = incomeData[i].RepairDate.ToShortDateString();
                worksheet.Cells[i + 2, 4].Value = incomeData[i].Price;
            }

            // Добавляем строку с итоговой суммой
            int lastRow = incomeData.Count + 2; // Последняя строка данных + 1
            worksheet.Cells[lastRow, 1].Value = "Total";
            worksheet.Cells[lastRow, 4].Value = totalSum;

            // Форматируем итоговую сумму
            worksheet.Cells[lastRow, 4].Style.Font.Bold = true;
            worksheet.Cells[lastRow, 4].Style.Numberformat.Format = "#,##0.00";

            // Авторазмер столбцов
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }

    // Отчет по остатку запчастей
    public byte[] GenerateSparePartsStockReportExcel()
    {
        var sparePartsData = _context.SpareParts
            .Select(sp => new
            {
                sp.PartName,
                sp.Articul,
                sp.Quantity,
                sp.Price
            })
            .ToList();

        using (var package = new OfficeOpenXml.ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Spare Parts Stock Report");

            // Заголовки столбцов
            worksheet.Cells[1, 1].Value = "Part Name";
            worksheet.Cells[1, 2].Value = "Articul";
            worksheet.Cells[1, 3].Value = "Quantity";
            worksheet.Cells[1, 4].Value = "Price";

            // Данные
            for (int i = 0; i < sparePartsData.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = sparePartsData[i].PartName;
                worksheet.Cells[i + 2, 2].Value = sparePartsData[i].Articul;
                worksheet.Cells[i + 2, 3].Value = sparePartsData[i].Quantity;
                worksheet.Cells[i + 2, 4].Value = sparePartsData[i].Price;
            }

            // Авторазмер столбцов
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }

    // Отчет по популярным видам работ
    public byte[] GeneratePopularOperationsReportExcel()
    {
        var popularOperationsData = _context.Orders
            .GroupBy(o => o.Operation.OperationName)
            .Select(g => new
            {
                OperationName = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .ToList();

        using (var package = new OfficeOpenXml.ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Popular Operations Report");

            // Заголовки столбцов
            worksheet.Cells[1, 1].Value = "Operation Name";
            worksheet.Cells[1, 2].Value = "Count";

            // Данные
            for (int i = 0; i < popularOperationsData.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = popularOperationsData[i].OperationName;
                worksheet.Cells[i + 2, 2].Value = popularOperationsData[i].Count;
            }

            // Авторазмер столбцов
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }

    // Отчет по популярным запчастям
    public byte[] GeneratePopularSparePartsReportExcel()
    {
        var popularSparePartsData = _context.OrderSpareParts
            .GroupBy(osp => osp.SparePart.PartName)
            .Select(g => new
            {
                PartName = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .ToList();

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Popular Spare Parts Report");

            // Заголовки столбцов
            worksheet.Cells[1, 1].Value = "Part Name";
            worksheet.Cells[1, 2].Value = "Count";

            // Данные
            for (int i = 0; i < popularSparePartsData.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = popularSparePartsData[i].PartName;
                worksheet.Cells[i + 2, 2].Value = popularSparePartsData[i].Count;
            }

            // Авторазмер столбцов
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}