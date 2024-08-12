using Domain.Core.ApiResponses;
using Domain.Modules.Employee.Interfaces;
using Domain.Modules.Employee.Models.Dtos;
using Domain.Modules.Employee.Models.Filters;
using Domain.Modules.Shift.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetEmployeeDto>>> GetEmployees([FromQuery] EmployeeFilter filter)
    {
        return await employeeService.GetEmployees(filter);
    }
    
    [HttpGet("get-statistics")]
    public async Task<Response<List<MonthlyStatisticsDto>>> GetStatistics([FromQuery] ShiftFilter filter)
    {
        return await employeeService.GetStatistics(filter);
    }

    [HttpPost]
    public async Task<Response<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto request)
    {
        return await employeeService.CreateEmployee(request);
    }

    [HttpPut]
    public async Task<Response<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto request)
    {
        return await employeeService.UpdateEmployee(request);
    }

    [HttpDelete]
    public async Task<Response<GetEmployeeDto>> DeleteEmployee(int id)
    {
        return await employeeService.DeleteEmployee(id);
    }
}
