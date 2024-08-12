using Domain.Modules.Employee.Models.Entities;
using Domain.Modules.Shift.Models.Dtos;

namespace Domain.Modules.Shift.Models.Entities;

public class Shift
{
    public int Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public int HoursWorked { get; set; }
    public int EmployeeId { get; set; }
    public virtual Employee.Models.Entities.Employee Employee { get; set; }

    public static Shift Create(StartShiftDto request)
    {
        return new Shift()
        {
            EmployeeId = request.Id,
            StartTime = request.StartTime.UtcDateTime,
        };
    }

    public void Update(EndShiftDto request)
    {
        EmployeeId = request.Id;
        EndTime = request.EndTime.UtcDateTime;
        HoursWorked = request.EndTime.UtcDateTime.Hour - StartTime.UtcDateTime.Hour;
    }
}
