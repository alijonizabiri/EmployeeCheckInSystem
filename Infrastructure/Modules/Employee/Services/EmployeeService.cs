using System.Net;
using Domain.Core.ApiResponses;
using Domain.Modules.Employee.Interfaces;
using Domain.Modules.Employee.Models.Dtos;
using Domain.Modules.Employee.Models.Enums;
using Domain.Modules.Employee.Models.Filters;
using Domain.Modules.Shift.Interfaces;
using Domain.Modules.Shift.Models;
using Infrastructure.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Modules.Employee.Services;

public class EmployeeService(
    IEmployeeRepository employeeRepository,
    IShiftRepository shiftRepository,
    ILogger<EmployeeService> logger) : IEmployeeService
{
    public async Task<Response<List<GetEmployeeDto>>> GetEmployees(EmployeeFilter filter)
    {
        var employees = await employeeRepository.GetEmployees(filter);
        var response = new Response<List<GetEmployeeDto>>()
        {
            StatusCode = 200,
            Data = employees.Select(e => new GetEmployeeDto()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
                Position = e.Position
            }).ToList()
        };

        return response;
    }

    public async Task<Response<List<MonthlyStatisticsDto>>> GetStatistics(ShiftFilter filter)
    {
        var shifts = await shiftRepository.GetShifts(filter);

        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(18, 0, 0);

        var problematicShifts = shifts.Where(sh =>
            sh.EndTime.HasValue && (sh.StartTime.TimeOfDay > startTime || sh.EndTime.Value.TimeOfDay < endTime) &&
            sh.Employee.Position != Position.CandleTester).ToList();

        problematicShifts.AddRange(shifts.Where(sh =>
            sh.StartTime.TimeOfDay > startTime || sh.EndTime.HasValue && 
            sh.EndTime.Value.TimeOfDay < endTime.Add(new TimeSpan(3, 0, 0)) && 
            sh.Employee.Position == Position.CandleTester).ToList());

        var groupByEmpId = problematicShifts.GroupBy(sh => sh.EmployeeId).ToList();

        var statistics = groupByEmpId
            .Select(sh => new MonthlyStatisticsDto()
            {
                EmployeeId = sh.Key,
                FullName = sh.Select(e => $"{e.Employee.FirstName} {e.Employee.LastName}").FirstOrDefault()!,
                TotalNotes = sh.Count()
            }).ToList();

        return new Response<List<MonthlyStatisticsDto>>(statistics);
    }

    public async Task<Response<GetEmployeeDto>> GetEmployeeById(int id)
    {
        var employee = await employeeRepository.GetEmployeeById(id);
        if (employee == null)
        {
            return new Response<GetEmployeeDto>(HttpStatusCode.BadRequest, "Employee not found.");
        }

        var converted = employee.ConvertToDto();

        return new Response<GetEmployeeDto>(converted);
    }

    public async Task<Response<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto request)
    {
        try
        {
            var employee = Domain.Modules.Employee.Models.Entities.Employee.Create(request);
            var result = await employeeRepository.CreateEmployee(employee);

            return result == 0
                ? new Response<GetEmployeeDto>(HttpStatusCode.BadRequest,
                    "Something went wrong while creating employee.")
                : new Response<GetEmployeeDto>(employee.ConvertToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }

    public async Task<Response<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto request)
    {
        try
        {
            var employee = await employeeRepository.GetEmployeeById(request.Id);
            if (employee == null)
            {
                return new Response<GetEmployeeDto>(HttpStatusCode.BadRequest, "Employee not found.");
            }

            employee.Update(request);
            var result = await employeeRepository.UpdateEmployee(employee);

            return result == 0
                ? new Response<GetEmployeeDto>(HttpStatusCode.BadRequest,
                    "Something went wrong while updating employee.")
                : new Response<GetEmployeeDto>(employee.ConvertToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }

    public async Task<Response<GetEmployeeDto>> DeleteEmployee(int id)
    {
        try
        {
            var employee = await employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return new Response<GetEmployeeDto>(HttpStatusCode.BadRequest, "Employee not found.");
            }

            var result = await employeeRepository.DeleteEmployee(employee);

            return result == 0
                ? new Response<GetEmployeeDto>(HttpStatusCode.BadRequest,
                    "Something went wrong while deleting employee.")
                : new Response<GetEmployeeDto>(employee.ConvertToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, "Internal server error.");
        }
    }
}
