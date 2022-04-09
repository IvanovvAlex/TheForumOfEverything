using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Services.Roles
{
    public interface IRoleService
    {
        Task<bool> CreateRole(string name);
        Task<bool> AddUserToRole(ApplicationUser user, string roleName);

    }
}
