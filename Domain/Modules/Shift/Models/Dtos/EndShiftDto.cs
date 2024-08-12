using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Shift.Models.Dtos;

public class EndShiftDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset EndTime { get; set; }
}
