using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Shift.Models.Dtos;

public class StartShiftDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset StartTime { get; set; }
}
