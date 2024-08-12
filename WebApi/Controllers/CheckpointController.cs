using Domain.Core.ApiResponses;
using Domain.Modules.Shift.Interfaces;
using Domain.Modules.Shift.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CheckpointController(IShiftService shiftService)
{
    [HttpPost("start-shift")]
    public async Task<Response<string>> StartShift(StartShiftDto request)
    {
        return await shiftService.StartShift(request);
    }
    
    [HttpPut("end-shift")]
    public async Task<Response<string>> EndShift(EndShiftDto request)
    {
        return await shiftService.EndShift(request);
    }
}
