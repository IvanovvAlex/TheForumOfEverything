using TheForumOfEverything.Models.Tags;

namespace TheForumOfEverything.Services.Tags
{
    public interface ITagService
    {
        Task<ICollection<TagViewModel>> GetAll();
        Task<TagViewModel> Create(CreateTagViewModel model);
        Task<TagViewModel> GetById(string id);
        Task<TagViewModel> Edit(TagViewModel model);
        Task<bool> DeleteById(string id);
    }
}
