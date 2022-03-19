using Microsoft.AspNetCore.Mvc;

namespace TheForumOfEverything.Controllers
{
    public class ApplicationUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
