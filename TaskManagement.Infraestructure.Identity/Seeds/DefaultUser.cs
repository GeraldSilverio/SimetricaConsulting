using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Application;

namespace TaskManagement.Infraestructure.Identity.Seeds;

public static class DefaultUser
{
    public static async Task CreateUser(UserManager<ApplicationUser> userManager)
    {
        try
        {
            ApplicationUser user = new()
            {
                UserName = "Gerald25",
                Email = "es.geraldsilverio@gmail.com",
                FirstName = "Gerald",
                LastName = "Silverio",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            // if (userManager.Users.All(x => x.Id != user.Id))
            // {
                var defaultUser = await userManager.FindByEmailAsync(user.Email);
                if (defaultUser == null)
                {
                    await userManager.CreateAsync(user, "123UserC#@");
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }
            // }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}