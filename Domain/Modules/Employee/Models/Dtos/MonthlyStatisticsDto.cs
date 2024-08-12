namespace Domain.Modules.Employee.Models.Dtos;

public class MonthlyStatisticsDto
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; }
    public int TotalNotes { get; set; } // in kalichestvo zamechaniy
}
