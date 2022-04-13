using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        public async Task<ICollection<UserViewModel>> GetUsers()
        {
            ICollection<UserViewModel> users = await context.ApplicationUser
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Username = x.UserName,
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            if (id == null)
            {
                return null;
            }
            ApplicationUser user = await context.ApplicationUser.Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }

            UserViewModel model = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Bio = user.Bio,
                Birthday = user.Birthday,
                Username = user.UserName,
                Email = user.Email,
            };

            return model;
        }

        public Task<UserViewModel> GetUserViewModel(ApplicationUser user)
        {
            if (user == null)
            {
                return null;
            }
            UserViewModel model = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Bio = user.Bio,
            };

            model.Posts = context.Posts.Where(p => p.UserId == user.Id && p.IsApproved).ToList();

            return Task.FromResult(model);
        }

        public async Task<UserViewModel> Edit(string userId, UserViewModel model)
        {
            ApplicationUser user = await context.ApplicationUser
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            if (model == null)
            {
                return null;
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Address = model.Address;
            user.Bio = model.Bio;
            user.Birthday = model.Birthday;
            context.SaveChanges();

            return model;
        }
    }
}
