﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Comments;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Comments;
using TheForumOfEverything.Services.Posts;

namespace TheForumOfEverything.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public PostsController(IPostService postService, ICommentService commentService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.context = context;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<PostViewModel> posts = await postService.GetAll();
            return View(posts);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Title");

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
        public async Task<IActionResult> Details(string id)
        {
            PostViewModel model = await postService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Title");

            PostViewModel model = await postService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            PostViewModel editedPost = await postService.Edit(model);
            if (editedPost == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Posts/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted = await postService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Posts/");
        }

        [Authorize]
        public async Task<IActionResult> Approve(string id)
        {
            bool isApproved = await postService.ApproveById(id);
            if (!isApproved)
            {
                return NotFound();
            }

            return Redirect($"/Administration/Home/ApprovePosts/");
        }
    }
}
