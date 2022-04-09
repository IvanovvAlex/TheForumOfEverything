using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Categories;

namespace TheForumOfEverything.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<CategoryViewModel>> GetAll()
        {
            ICollection<CategoryViewModel> categories = await context.Categories
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToListAsync();

            return categories;
        }
        public async Task<ICollection<CategoryViewModel>> GetLastNCategories(int n)
        {
            ICollection<CategoryViewModel> categories = context.Categories
                .Skip(Math.Max(0, context.Categories.Count() - n))
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToHashSet();
            return categories;
        }
        public async Task<CategoryViewModel> Create(CreateCategoryViewModel model, string userId)
        {
            string modelTitle = model.Title;

            bool isCategoryExist = await context.Categories.AnyAsync(x => x.Title == modelTitle);
            if (isCategoryExist)
            {
                return null;
            }

            Category newCategory = new Category()
            {
                Title = modelTitle,
            };
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();

          
            string newCategoryId = newCategory.Id;
            CategoryViewModel newCategoryViewModel = new CategoryViewModel()
            {
                Id = newCategoryId,
            };
            return newCategoryViewModel;
        }

        public async Task<CategoryViewModel> GetById(string id)
        {
            Category category = await context.Categories.Include(p => p.Posts.Where(x =>x.IsApproved)).FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }

            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Posts = category.Posts.OrderByDescending(x => x.TimeCreated).ToList()
            };

            return model;
        }

        public async Task<CategoryViewModel> Edit(CategoryViewModel model)
        {
            string modelId = model.Id;

            Category category = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (category == null)
            {
                return null;
            }

            category.Title = model.Title;
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteById(string id)
        {
            Category category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return false;
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
