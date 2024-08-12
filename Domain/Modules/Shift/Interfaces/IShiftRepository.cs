using Domain.Modules.Shift.Models;

namespace Domain.Modules.Shift.Interfaces;

public interface IShiftRepository
{
    Task<List<Models.Entities.Shift>> GetShifts(ShiftFilter filter); 
    Task<int> CreateShift(Models.Entities.Shift shift);
    Task<int> UpdateShift(Models.Entities.Shift shift);
}
