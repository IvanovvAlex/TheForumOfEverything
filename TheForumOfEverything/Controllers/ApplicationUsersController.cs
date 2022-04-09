using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;
using TheForumOfEverything.Services.ApplicationUsers;
using TheForumOfEverything.Services.Roles;

namespace TheForumOfEverything.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleService;

        public ApplicationUsersController(IApplicationUserService userService, UserManager<ApplicationUser> userManager, IRoleService roleService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.roleService = roleService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            UserViewModel model = await userService.GetUserViewModel(user);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            UserViewModel model = await userService.GetUser(id);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserViewModel editedUser = await userService.Edit(userId, model);
            if (editedUser == null)
            {
                return NotFound();
            }

            return Redirect($"/ApplicationUsers/MyProfile/");
        }
    }
}
