using Microsoft.AspNetCore.Identity;

namespace RequestAndDelivery.Seeds
{
    public static class DefaultRoles
    {

        public static async Task AddAsync(RoleManager<IdentityRole> _roleManager)
        {
           if(!_roleManager.Roles.Any())
           {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
           }
        }
    }
}
