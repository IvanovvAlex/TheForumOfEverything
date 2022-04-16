using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Areas.Administration.Services;
using TheForumOfEverything.Areas.Administration.Services.ApplicationUsers;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Models.Users;

namespace TheForumOfEverything.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IPostAdminService postService;
        private readonly IApplicationUserAdminService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public HomeController(IPostAdminService postService, IApplicationUserAdminService userService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
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
        public async Task<IActionResult> Users()
        {
            ICollection<UserViewModel> users = await userService.GetUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmins(string SearchString)
        {
            ICollection<UserViewModel> users = await userService.Search(SearchString);
            return View(users);
        }
        public async Task<IActionResult> ApprovePosts()
        {
            ICollection<PostViewModel> posts = await postService.GetAll();
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> ApprovePosts(string SearchString)
        {
            ICollection<PostViewModel> posts = await postService.Search(SearchString);
            return View(posts);
        }
    }
}
