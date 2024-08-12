using Domain.Modules.Employee.Models.Dtos;
using Domain.Modules.Employee.Models.Enums;

namespace Domain.Modules.Employee.Models.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Position Position { get; set; }
    public virtual List<Shift.Models.Entities.Shift> Shifts { get; set; }

    public static Employee Create(CreateEmployeeDto request)
    {
        return new Employee()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Position = request.Position,
        };
    }

    public void Update(UpdateEmployeeDto request)
    {
        FirstName = request.FirstName;
        LastName = request.LastName;
        MiddleName = request.MiddleName;
        Position = request.Position;
    }
}
