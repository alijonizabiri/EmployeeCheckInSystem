using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Modules.Shift.Models.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Entities.Shift>
{
    public void Configure(EntityTypeBuilder<Entities.Shift> builder)
    {
        
    }
}
