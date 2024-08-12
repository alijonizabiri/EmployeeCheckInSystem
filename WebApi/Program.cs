using Domain.Modules.Employee.Interfaces;
using Domain.Modules.Shift.Interfaces;
using Infrastructure.Core.Persistence;
using Infrastructure.Modules.Employee.Repositories;
using Infrastructure.Modules.Employee.Services;
using Infrastructure.Modules.Shift.Repositories;
using Infrastructure.Modules.Shift.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllerRoute(
    name: "default",    
    pattern: "{controller}/{action=Index}/{id?}");
app.Run();
