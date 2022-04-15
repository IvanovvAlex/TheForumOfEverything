using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Areas.Administration.Services.ApplicationUsers
{
    public interface IApplicationUserAdminService
    {
        Task<ICollection<UserViewModel>> GetUsers();
        Task<ICollection<UserViewModel>> Search(string searchString);
    }
}
