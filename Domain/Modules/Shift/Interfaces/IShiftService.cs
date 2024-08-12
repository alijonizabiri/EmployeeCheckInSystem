using Domain.Core.ApiResponses;
using Domain.Modules.Shift.Models.Dtos;

namespace Domain.Modules.Shift.Interfaces;

public interface IShiftService
{
    Task<Response<string>> StartShift(StartShiftDto request);
    Task<Response<string>> EndShift(EndShiftDto request);
}
