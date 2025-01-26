using API_UP.Data;
using API_UP.Models;
using API_UP.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MyDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeAccountService>();
builder.Services.AddScoped<OperationService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderSparePartService>();
builder.Services.AddScoped<PositionService>();
builder.Services.AddScoped<QualificationService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<ServiceStationService>();
builder.Services.AddScoped<SparePartService>();


var app = builder.Build();

// Настройка middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Маршрутизация для контроллеров
app.MapControllers();

app.Run();