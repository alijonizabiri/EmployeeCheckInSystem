using Domain.Core.ApiResponses;
using Domain.Modules.Employee.Models.Dtos;
using Domain.Modules.Employee.Models.Filters;
using Domain.Modules.Shift.Models;

namespace Domain.Modules.Employee.Interfaces;

public interface IEmployeeService
{
    Task<Response<List<GetEmployeeDto>>> GetEmployees(EmployeeFilter filter);
    Task<Response<GetEmployeeDto>> GetEmployeeById(int id);
    Task<Response<List<MonthlyStatisticsDto>>> GetStatistics(ShiftFilter filter);
    Task<Response<GetEmployeeDto>> CreateEmployee(CreateEmployeeDto request);
    Task<Response<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto request);
    Task<Response<GetEmployeeDto>> DeleteEmployee(int id);
}
