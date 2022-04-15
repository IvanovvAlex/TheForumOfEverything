using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Areas.Administration.Services
{
    public interface IPostAdminService
    {
        Task<ICollection<PostViewModel>> GetAll();
        Task<ICollection<PostViewModel>> Search(string SearchString);
        Task<ICollection<PostViewModel>> GetLastNPosts(int n);
        Task<PostViewModel> Create(CreatePostViewModel model, string userId);
        Task<PostViewModel> GetById(string id);
        Task<PostViewModel> Edit(PostViewModel model);
        Task<bool> DeleteById(string id);
    }
}
