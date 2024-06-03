using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Application;

namespace TaskManagement.Infraestructure.Identity.Seeds;

public class DefaultRoles
{
    public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
    }
}