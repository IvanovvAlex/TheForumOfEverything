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
        public ICollection<CategoryViewModel> GetAll()
        {
            ICollection<CategoryViewModel> categories = context.Categories
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToHashSet();

            return categories;
        }
        public ICollection<CategoryViewModel> GetLastNCategories(int n)
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

            bool isCategoryExist = context.Categories.Any(x => x.Title == modelTitle);
            if (isCategoryExist)
            {
                return null;
            }

            Category newCategory = new Category()
            {
                Title = modelTitle,
            };
            context.Categories.Add(newCategory);
            context.SaveChanges();

          
            string newCategoryId = newCategory.Id;
            CategoryViewModel newCategoryViewModel = new CategoryViewModel()
            {
                Id = newCategoryId,
            };
            return newCategoryViewModel;
        }

        public CategoryViewModel GetById(string id)
        {
            Category category = context.Categories.Include(p => p.Posts).FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return null;
            }

            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Posts = category.Posts
            };

            return model;
        }

        public CategoryViewModel Edit(CategoryViewModel model)
        {
            string modelId = model.Id;

            Category category = context.Categories
                .FirstOrDefault(x => x.Id == modelId);
            if (category == null)
            {
                return null;
            }

            category.Title = model.Title;
            context.SaveChanges();

            return model;
        }

        public bool DeleteById(string id)
        {
            Category category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return false;
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
    }
}
