using Domain.Modules.Shift.Interfaces;
using Domain.Modules.Shift.Models;
using Infrastructure.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Modules.Shift.Repositories;

public class ShiftRepository(ApplicationDbContext context, ILogger<ShiftRepository> logger) : IShiftRepository
{
    public async Task<List<Domain.Modules.Shift.Models.Entities.Shift>> GetShifts(ShiftFilter filter)
    {
        var query = context.Shifts.AsQueryable();
        if (filter.Month != null)
        {
            query = query.Where(sh => sh.StartTime.Month == filter.Month);
        }
        if (filter.Year != null)
        {
            query = query.Where(sh => sh.StartTime.Year == filter.Year);
        }

        return await query.ToListAsync();
    }

    public async Task<int> CreateShift(Domain.Modules.Shift.Models.Entities.Shift shift)
    {
        try
        {
            await context.Shifts.AddAsync(shift);
            return await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return 0;
        }
    }

    public async Task<int> UpdateShift(Domain.Modules.Shift.Models.Entities.Shift shift)
    {
        try
        {
            context.Shifts.Update(shift);
            return await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return 0;
        }
    }
}
