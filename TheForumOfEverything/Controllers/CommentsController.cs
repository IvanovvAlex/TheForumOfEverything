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
        public IActionResult Index()
        {
            ICollection<CommentViewModel> comments = commentService.GetAll();
            return View(comments);
        }

        //[Authorize]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateCommentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return NotFound();
        //    }
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    CommentViewModel newCommentModel = await commentService.Create(model, userId);

        //    if (newCommentModel != null)
        //    {
        //        return Redirect($"/Comments/Details/{newCommentModel.Id}");
        //    }
        //    return View(model);
        //}

        [Authorize]
        public IActionResult Details(string id)
        {
            CommentViewModel model = commentService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            CommentViewModel model = commentService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CommentViewModel model)
        {
            CommentViewModel editedComment = commentService.Edit(model);
            if (editedComment == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Comments/Details/{id}");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            bool isDeleted = commentService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Comments/");
        }
    }
}
