using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheForumOfEverything.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Posts;

namespace TheForumOfEverything.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService postService;

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            this.postService = postService;
        }

        public IActionResult Index()
        {
            ICollection<PostViewModel> posts = postService.GetLastNPosts(5);
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}