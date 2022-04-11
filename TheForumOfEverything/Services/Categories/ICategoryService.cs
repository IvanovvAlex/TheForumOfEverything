using TheForumOfEverything.Models.Categories;

namespace TheForumOfEverything.Services.Categories
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAll();
        Task<ICollection<CategoryViewModel>> GetLastNCategories(int n);
        Task<CategoryViewModel> Create(CreateCategoryViewModel model);
        Task<CategoryViewModel> GetById(string id);
        Task<CategoryViewModel> Edit(CategoryViewModel model);
        Task<bool> DeleteById(string id);
    }
}
