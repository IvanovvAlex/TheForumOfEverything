using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Services.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<ICollection<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUserViewModel(ApplicationUser user);
        Task<UserViewModel> Edit(string userId, UserViewModel user);
        Task<UserViewModel> GetUser( string id );
    }
}
