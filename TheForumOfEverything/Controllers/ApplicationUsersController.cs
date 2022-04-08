using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;
using TheForumOfEverything.Services.ApplicationUsers;

namespace TheForumOfEverything.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUsersController(IApplicationUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            UserViewModel model = await userService.GetUser(user);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            UserViewModel model = await userService.GetUser(user);

            return View(model);
        }
    }
}
