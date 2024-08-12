using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Employee.Models.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Entities.Employee>
{
    public void Configure(EntityTypeBuilder<Entities.Employee> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(20).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(20).IsRequired();
        builder.Property(e => e.MiddleName).IsRequired(false);
    }
}
