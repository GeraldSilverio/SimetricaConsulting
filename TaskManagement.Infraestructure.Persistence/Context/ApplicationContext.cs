using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Infraestructure.Persistence.EntitiesConfiguration;
using TaskStatus = TaskManagement.Core.Domain.Entities.TaskStatus;

namespace TaskManagement.Infraestructure.Persistence.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskStatusConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TaskStatus> TaskStatus { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
}