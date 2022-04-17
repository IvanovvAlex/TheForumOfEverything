using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Comments;
using TheForumOfEverything.Services.Comments;

namespace TheForumOfEverything.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ICollection<CommentViewModel> comments = await commentService.GetAll();
            return View(comments);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentViewModel model)
        {
            var postId = model.PostId;

            if (!ModelState.IsValid)
            {
                return Redirect($"/Posts/Details/{postId}");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.GetUserAsync(HttpContext.User);
            CommentViewModel newCommentModel = await commentService.Create(model, user, userId, postId);

            if (newCommentModel != null)
            {
                return Redirect($"/Posts/Details/{postId}");
            }
            return View(model);
        }

        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Details(string id)
        {
            CommentViewModel model = await commentService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Edit(string id)
        {
            CommentViewModel model = await commentService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = "Developer")]
        [HttpPost]
        public async Task<IActionResult> Edit(CommentViewModel model)
        {
            CommentViewModel editedComment = await commentService.Edit(model);
            if (editedComment == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Comments/Details/{id}");
        }

        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted = await commentService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Comments/");
        }
    }
}
