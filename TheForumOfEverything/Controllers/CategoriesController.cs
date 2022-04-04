using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Categories;
using TheForumOfEverything.Services.Categories;
using TheForumOfEverything.Services.Comments;

namespace TheForumOfEverything.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            this.categoryService = categoryService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            ICollection<CategoryViewModel> categories = categoryService.GetAll();
            return View(categories);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            CategoryViewModel newCategoryModel = await categoryService.Create(model, userId);

            if (newCategoryModel != null)
            {
                return Redirect($"/Categories/Details/{newCategoryModel.Id}");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            CategoryViewModel model = categoryService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            CategoryViewModel model = categoryService.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            CategoryViewModel editedCategory = categoryService.Edit(model);
            if (editedCategory == null)
            {
                return NotFound();
            }
            string id = model.Id;

            return Redirect($"/Categories/Details/{id}");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            bool isDeleted = categoryService.DeleteById(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Redirect($"/Categories/");
        }
    }
}
