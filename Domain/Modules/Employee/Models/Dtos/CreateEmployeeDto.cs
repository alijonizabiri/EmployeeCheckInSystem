using System.ComponentModel.DataAnnotations;
using Domain.Modules.Employee.Models.Enums;

namespace Domain.Modules.Employee.Models.Dtos;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    public string? MiddleName { get; set; }
    [Required(ErrorMessage = "Position is required")]
    public Position Position { get; set; }
}
