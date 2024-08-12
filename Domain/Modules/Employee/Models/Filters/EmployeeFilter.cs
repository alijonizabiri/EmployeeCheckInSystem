using Domain.Modules.Employee.Models.Enums;

namespace Domain.Modules.Employee.Models.Filters;

public class EmployeeFilter
{
    public string? FullName { get; set; }
    public Position? Position { get; set; }
}
