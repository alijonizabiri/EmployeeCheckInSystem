using Domain.Modules.Employee.Interfaces;
using Domain.Modules.Employee.Models.Filters;
using Infrastructure.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Modules.Employee.Repositories;

public class EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger) : IEmployeeRepository
{
    public async Task<List<Domain.Modules.Employee.Models.Entities.Employee>> GetEmployees(EmployeeFilter filter)
    {
        var query = context.Employees.AsQueryable();
        if (filter.FullName != null)
        {
            query = query.Where(e => string.Concat(e.FirstName, e.LastName).ToLower().Trim()
                .Contains(filter.FullName.ToLower().Trim()));
        }

        if (filter.Position != null)
        {
            query = query.Where(e => e.Position == filter.Position);
        }

        return await query.ToListAsync();
    }

    public async Task<Domain.Modules.Employee.Models.Entities.Employee> GetEmployeeById(int id)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        
        return employee;
    }
    public async Task<int> CreateEmployee(Domain.Modules.Employee.Models.Entities.Employee request)
    {
        try
        {
            await context.Employees.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return 0;
        }
    }

    public async Task<int> UpdateEmployee(Domain.Modules.Employee.Models.Entities.Employee request)
    {
        try
        {
            context.Employees.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> DeleteEmployee(Domain.Modules.Employee.Models.Entities.Employee request)
    {
        try
        {
            context.Employees.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}
