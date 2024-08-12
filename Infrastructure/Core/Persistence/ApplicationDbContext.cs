
using Domain.Modules.Employee.Models.Configurations;
using Domain.Modules.Employee.Models.Entities;
using Domain.Modules.Shift.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
