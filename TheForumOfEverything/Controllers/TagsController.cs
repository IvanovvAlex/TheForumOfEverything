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

        [Authorize]
        public IActionResult Index()
        {
            ICollection<TagViewModel> tags = tagService.GetAll();
            return View(tags);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            TagViewModel newTagModel = tagService.Create(model);

            if (newTagModel != null)
            {
                return Redirect($"/Tags/Details/{newTagModel.Id}");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            TagViewModel model = tagService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            TagViewModel model = tagService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(TagViewModel model)
        {
            TagViewModel editedTag = tagService.Edit(model);
            if (editedTag == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Tags/Details/{id}");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            bool isDeleted = tagService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Tags/");
        }
    }
}
