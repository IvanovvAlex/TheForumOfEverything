using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Services.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<UserViewModel> GetUser(ApplicationUser user);
    }
}
