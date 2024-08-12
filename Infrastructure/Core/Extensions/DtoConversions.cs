
using Domain.Modules.Employee.Models.Dtos;
using Domain.Modules.Employee.Models.Entities;

namespace Infrastructure.Core.Extensions;

public static class DtoConversions
{
    public static List<GetEmployeeDto> ConvertToDto(this List<Employee> employees)
    {
        return (from employee in employees
            select new GetEmployeeDto()
            {
                Id = employee.Id,
                Position = employee.Position,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName
            }).ToList();
    }

    public static GetEmployeeDto ConvertToDto(this Employee employee)
    {
        return new GetEmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            Position = employee.Position
        };
    }
}
