using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Areas.Administration.Services;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IPostAdminService postService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public HomeController(IPostAdminService postService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }
        public async Task<IActionResult> AddAdmins()
        {
            return View();
        }
        public async Task<IActionResult> ApprovePosts()
        {
            ICollection<PostViewModel> posts = await postService.GetAll();

            return View(posts);
        }
    }
}
