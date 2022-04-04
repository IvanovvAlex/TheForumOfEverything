using TheForumOfEverything.Models.Categories;

namespace TheForumOfEverything.Services.Categories
{
    public interface ICategoryService
    {
        ICollection<CategoryViewModel> GetAll();
        ICollection<CategoryViewModel> GetLastNCategories(int n);
        Task<CategoryViewModel> Create(CreateCategoryViewModel model, string userId);
        CategoryViewModel GetById(string id);
        CategoryViewModel Edit(CategoryViewModel model);
        bool DeleteById(string id);
    }
}
