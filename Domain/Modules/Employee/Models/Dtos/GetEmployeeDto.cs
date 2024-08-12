using Domain.Modules.Employee.Models.Enums;

namespace Domain.Modules.Employee.Models.Dtos;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Position Position { get; set; }
}
