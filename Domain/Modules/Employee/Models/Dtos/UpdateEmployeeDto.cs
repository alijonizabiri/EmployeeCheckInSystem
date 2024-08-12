using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Employee.Models.Dtos;

public class UpdateEmployeeDto : CreateEmployeeDto
{
    [Required]
    public int Id { get; set; }
}
