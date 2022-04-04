using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Posts;

namespace TheForumOfEverything.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            ICollection<PostViewModel> posts = postService.GetAll();
            return View(posts);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PostViewModel newPostModel = await postService.Create(model, userId);

            if (newPostModel != null)
            {
                return Redirect($"/Posts/Details/{newPostModel.Id}");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            PostViewModel model = postService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            PostViewModel model = postService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostViewModel model)
        {
            PostViewModel editedPost = postService.Edit(model);
            if (editedPost == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Tags/Details/{id}");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            bool isDeleted = postService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Posts/");
        }
    }
}
