using TheForumOfEverything.Models.Tags;

namespace TheForumOfEverything.Services.Tags
{
    public interface ITagService
    {
        ICollection<TagViewModel> GetAll();
        bool CreateTag(CreateTagViewModel model);
        TagViewModel GetById(string Id);
        TagViewModel Edit(TagViewModel model);
        bool DeleteById(string Id);
    }
}
