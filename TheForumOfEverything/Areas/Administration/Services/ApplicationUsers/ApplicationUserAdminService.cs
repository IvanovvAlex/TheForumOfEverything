using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Areas.Administration.Services.ApplicationUsers
{
    public class ApplicationUserAdminService : IApplicationUserAdminService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUserAdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<ICollection<UserViewModel>> GetUsers()
        {
            ICollection<UserViewModel> users = await context.ApplicationUser
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Username = x.UserName,
                })
                .OrderByDescending(x => x.Username)
                .ToListAsync();

            return users;
        }
        public async Task<ICollection<UserViewModel>> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
            {
                return await this.GetUsers();
            }
            ICollection<UserViewModel> users = await context.ApplicationUser
                            .Where(x => x.Email.Contains(searchString))
                            .Select(x => new UserViewModel()
                            {
                                Id = x.Id,
                                Username = x.UserName,

                            })
                            .OrderBy(x => x.Username)
                            .ToListAsync();

            return users;
        }
    }
}
