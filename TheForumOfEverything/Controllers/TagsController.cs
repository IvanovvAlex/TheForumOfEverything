using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheForumOfEverything.Models.Tags;
using TheForumOfEverything.Services.Tags;

namespace TheForumOfEverything.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagService tagService;
        private readonly UserManager<IdentityUser> userManager;

        public TagsController(ITagService tagService, UserManager<IdentityUser> userManager)
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

            bool isTagCreated = tagService.CreateTag(model);

            if (isTagCreated)
            {
                return RedirectToAction("Index", "Tags");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Details(string Id)
        {
            TagViewModel model = tagService.GetById(Id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string Id)
        {
            TagViewModel model = tagService.GetById(Id);
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
            string Id = model.Id;

            return Redirect($"/Tags/Details/{Id}");
        }

        [Authorize]
        public IActionResult Delete(string Id)
        {
            bool isDeleted = tagService.DeleteById(Id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Tags/");
        }
    }
}
