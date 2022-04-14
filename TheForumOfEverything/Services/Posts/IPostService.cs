using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Services.Posts
{
    public interface IPostService
    {
        Task<ICollection<PostViewModel>> GetAll();
        Task<ICollection<Post>> GetAllFromCategory(string categoryId);
        Task<ICollection<PostViewModel>> GetLastNPosts(int n);
        Task<ICollection<PostViewModel>> Search(string searchString);
        Task<PostViewModel> Create(CreatePostViewModel model, string userId);
        Task<PostViewModel> GetById(string id);
        Task<PostViewModel> Edit(PostViewModel model);
        Task<bool> DeleteById(string id);
        Task<bool> ApproveById(string id);
    }
}
