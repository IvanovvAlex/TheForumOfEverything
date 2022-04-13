using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Areas.Administration.Services;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.ApplicationUsers;

namespace TheForumOfEverything.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IPostAdminService postService;
        private readonly IApplicationUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public HomeController(IPostAdminService postService, IApplicationUserService userService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.postService = postService;
            this.userService = userService;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> AddAdmins()
        {
            var users = await userService.GetUsers();
            return View(users);
        }
        public async Task<IActionResult> ApprovePosts()
        {
            ICollection<PostViewModel> posts = await postService.GetAll();

            return View(posts);
        }
    }
}
