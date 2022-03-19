using TheForumOfEverything.Models.Tags;

namespace TheForumOfEverything.Services.Tags
{
    public interface ITagService
    {
        ICollection<TagViewModel> GetAll();
        TagViewModel Create(CreateTagViewModel model);
        TagViewModel GetById(string id);
        TagViewModel Edit(TagViewModel model);
        bool DeleteById(string id);
    }
}
