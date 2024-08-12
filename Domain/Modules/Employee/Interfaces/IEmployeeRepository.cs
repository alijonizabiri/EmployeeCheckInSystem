using Domain.Modules.Employee.Models.Filters;
namespace Domain.Modules.Employee.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Models.Entities.Employee>> GetEmployees(EmployeeFilter filter);
    Task<Models.Entities.Employee> GetEmployeeById(int id);
    Task<int> CreateEmployee(Models.Entities.Employee request);
    Task<int> UpdateEmployee(Models.Entities.Employee request);
    Task<int> DeleteEmployee(Models.Entities.Employee request);
}
