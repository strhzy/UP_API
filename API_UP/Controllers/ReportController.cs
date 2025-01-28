using API_UP.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_UP.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // Отчет по доходам
    [HttpGet("income/excel")]
    public IActionResult GetIncomeReportExcel([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var excelBytes = _reportService.GenerateIncomeReportExcel(startDate, endDate);
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IncomeReport.xlsx");
    }

    // Отчет по остатку запчастей
    [HttpGet("spareparts/stock/excel")]
    public IActionResult GetSparePartsStockReportExcel()
    {
        var excelBytes = _reportService.GenerateSparePartsStockReportExcel();
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SparePartsStockReport.xlsx");
    }

    // Отчет по популярным видам работ
    [HttpGet("operations/popular/excel")]
    public IActionResult GetPopularOperationsReportExcel()
    {
        var excelBytes = _reportService.GeneratePopularOperationsReportExcel();
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PopularOperationsReport.xlsx");
    }

    // Отчет по популярным запчастям
    [HttpGet("spareparts/popular/excel")]
    public IActionResult GetPopularSparePartsReportExcel()
    {
        var excelBytes = _reportService.GeneratePopularSparePartsReportExcel();
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PopularSparePartsReport.xlsx");
    }
}