using Microsoft.AspNetCore.Mvc;

namespace TheForumOfEverything.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
