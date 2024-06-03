using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskStatus = TaskManagement.Core.Domain.Entities.TaskStatus;

namespace TaskManagement.Infraestructure.Persistence.EntitiesConfiguration;

public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
{
    public void Configure(EntityTypeBuilder<TaskStatus> builder)
    {
        var boolConverter = new ValueConverter<bool, int>(
            v => v ? 1 : 0,
            v => v == 1);

        builder.ToTable("TaskStatus");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(30);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.IsActive).HasConversion(boolConverter);
        
        builder.HasData(
            new TaskStatus[]
            {
                new TaskStatus()
                {
                    Id = 1,
                    Name = "Nueva",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsActive = true,
                },
                new TaskStatus()
                {
                    Id = 2,
                    Name = "En proceso",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsActive = true,
                },
                new TaskStatus()
                {
                    Id = 3,
                    Name = "Completada",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsActive = true,
                }
            }
        );
    }
}