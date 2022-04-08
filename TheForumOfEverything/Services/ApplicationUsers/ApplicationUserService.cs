using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Services.ApplicationUsers
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task<UserViewModel> GetUser(ApplicationUser user)
        {
            UserViewModel model = new UserViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Bio = user.Bio,
            };

            model.Posts = context.Posts.Where(p => p.UserId == user.Id).ToList();

            return Task.FromResult(model);
        }
    }
}
