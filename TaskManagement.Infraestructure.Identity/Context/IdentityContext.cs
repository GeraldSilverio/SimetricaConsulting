using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TaskManagement.Infraestructure.Identity.Context;

/// <summary>
/// Clase la cual representa el DbContext del contexto de Identity, con su constructor primario.
/// </summary>
/// <param name="options"></param>
public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("SYSTEM");

        var boolConverter = new ValueConverter<bool, int>(
            v => v ? 1 : 0,
            v => v == 1);


        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "Users");
            //Descomentar, para poder usarlo en Oracle.
            //entity.Property(x => x.IsActive).HasColumnType("NUMBER(1)").HasConversion(boolConverter);
            //entity.Property(x => x.EmailConfirmed).HasColumnType("NUMBER(1)").HasConversion(boolConverter);
            //entity.Property(x => x.PhoneNumberConfirmed).HasColumnType("NUMBER(1)").HasConversion(boolConverter);
            //entity.Property(x => x.LockoutEnabled).HasColumnType("NUMBER(1)").HasConversion(boolConverter);
            //entity.Property(x => x.TwoFactorEnabled).HasColumnType("NUMBER(1)").HasConversion(boolConverter);
        });

        
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Roles");
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "UserRoles");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "UserLogin");
        });
    }
}
