using Microsoft.AspNetCore.Mvc;

namespace TheForumOfEverything.Controllers
{
    public class UserAccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
