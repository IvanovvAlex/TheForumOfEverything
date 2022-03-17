using Microsoft.AspNetCore.Mvc;

namespace TheForumOfEverything.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
