using Microsoft.AspNetCore.Identity;
using RequestAndDelivery.Data.Domain_Models;

namespace RequestAndDelivery.Seeds
{
    public static class DefaultUsers
    {
        public static async Task AddAdminUserAsync(UserManager<ApplicationUser> _userManager)
        {
            var admin = new ApplicationUser
            {
                FullName = "karam",
                UserName = "karam",
                Email = "Admin@ReqDeliv.com",
                EmailConfirmed = true
            };
            var user = await _userManager.FindByEmailAsync(admin.UserName);
            if(user == null)
            {
                await _userManager.CreateAsync(admin, "P@ssw0rd");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }

    }
}
