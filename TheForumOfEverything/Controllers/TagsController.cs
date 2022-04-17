using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Tags;
using TheForumOfEverything.Services.Tags;

namespace TheForumOfEverything.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagService tagService;
        private readonly UserManager<ApplicationUser> userManager;

        public TagsController(ITagService tagService, UserManager<ApplicationUser> userManager)
        {
            this.tagService = tagService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<TagViewModel> tags = await tagService.GetAll();
            return View(tags);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            TagViewModel newTagModel = await tagService.Create(model);

            if (newTagModel != null)
            {
                return Redirect($"/Tags/Details/{newTagModel.Id}");
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            TagViewModel model = await tagService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            TagViewModel model = await tagService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(TagViewModel model)
        {
            TagViewModel editedTag = await tagService.Edit(model);
            if (editedTag == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Tags/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            bool isDeleted = await tagService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Tags/");
        }
    }
}
