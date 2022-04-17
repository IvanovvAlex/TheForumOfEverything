using Microsoft.AspNetCore.Identity;
using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Services.Roles
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<bool> CreateRole(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return false; 
            }
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            });
            return true;
        }
        public async Task<bool> AddUserToRole(ApplicationUser user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);
            return true;
        }
    }
}
