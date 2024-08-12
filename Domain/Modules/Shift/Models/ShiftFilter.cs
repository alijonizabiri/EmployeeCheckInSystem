using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Shift.Models;

public class ShiftFilter
{
    public int? Month { get; set; }
    public int? Year { get; set; }
}
