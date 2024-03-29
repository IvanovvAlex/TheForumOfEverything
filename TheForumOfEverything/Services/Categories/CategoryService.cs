﻿using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Categories;
using TheForumOfEverything.Services.Posts;

namespace TheForumOfEverything.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IPostService postService;
        public CategoryService(ApplicationDbContext context, IPostService postService)
        {
            this.context = context;
            this.postService = postService;
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
            ICollection<CategoryViewModel> categories = await context.Categories
                .Skip(Math.Max(0, context.Categories.Count() - n))
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToListAsync();
            return categories;
        }
        public async Task<CategoryViewModel> Create(CreateCategoryViewModel model)
        {
            if (model == null)
            {
                return null;
            }
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
            if (id == null)
            {
                return null;
            }
            Category category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }

            var posts = await postService.GetAllFromCategory(id);

            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Posts = (ICollection<Post>)posts
            };

            return model;
        }

        public async Task<CategoryViewModel> Edit(CategoryViewModel model)
        {
            if (model == null)
            {
                return null;
            }

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
            if (id == null)
            {
                return false;
            }
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
