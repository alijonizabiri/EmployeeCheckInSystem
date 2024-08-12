using System.Net;
using Domain.Core.ApiResponses;
using Domain.Modules.Employee.Interfaces;
using Domain.Modules.Shift.Interfaces;
using Domain.Modules.Shift.Models.Dtos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Modules.Shift.Services;

public class ShiftService(
    IEmployeeRepository employeeRepository,
    IShiftRepository shiftRepository,
    ILogger<ShiftService> logger) : IShiftService
{
    public async Task<Response<string>> StartShift(StartShiftDto request)
    {
        try
        {
            var employee = await employeeRepository.GetEmployeeById(request.Id);
            if (employee == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Employee not found.");
            }

            if (employee.Shifts.Any(sh => sh.StartTime.Date == request.StartTime.Date))
            {
                return new Response<string>(HttpStatusCode.BadRequest,
                    $"The employee started the shift on this date: {request.StartTime.Date:dd MMMM yyyy}");
            }

            var lastShift = employee.Shifts?.OrderByDescending(sh => sh.StartTime)
                .FirstOrDefault(sh => sh.EndTime == null);
            if (lastShift != null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Shift already started.");
            }

            var shift = Domain.Modules.Shift.Models.Entities.Shift.Create(request);
            var result = await shiftRepository.CreateShift(shift);

            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong while starting shift.")
                : new Response<string>("Shift started.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task<Response<string>> EndShift(EndShiftDto request)
    {
        try
        {
            var employee = await employeeRepository.GetEmployeeById(request.Id);
            if (employee == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Employee not found.");
            }

            var lastShift = employee.Shifts?.OrderByDescending(sh => sh.StartTime)
                .FirstOrDefault(sh => sh.EndTime == null);
            if (lastShift == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Employee doesn't have an active shifts.");
            }

            lastShift.Update(request);
            var result = await shiftRepository.UpdateShift(lastShift);

            return result == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong while ending shift.")
                : new Response<string>("Shift finished.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal server error");
        }
    }
}
