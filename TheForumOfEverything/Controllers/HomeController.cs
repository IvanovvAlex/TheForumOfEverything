using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models;
using TheForumOfEverything.Models.Categories;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Models.Shared;
using TheForumOfEverything.Services.Categories;
using TheForumOfEverything.Services.Posts;
using TheForumOfEverything.Services.Shared;

namespace TheForumOfEverything.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService postService;
        private readonly ISharedService sharedService;
        private readonly ICategoryService categoryService;
        private RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, IPostService postService, ICategoryService categoryService, ISharedService sharedService)
        {
            _logger = logger;
            this.postService = postService;
            this.categoryService = categoryService;
            this.roleManager = roleManager;
            this.sharedService = sharedService;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<CategoryViewModel> categories = await categoryService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            await sharedService.EmailSender(model);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
