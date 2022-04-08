using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Services.Posts
{
    public interface IPostService
    {
        Task<ICollection<PostViewModel>> GetAll();
        Task<ICollection<PostViewModel>> GetLastNPosts(int n);
        Task<PostViewModel> Create(CreatePostViewModel model, string userId);
        Task<PostViewModel> GetById(string id);
        Task<PostViewModel> Edit(PostViewModel model);
        Task<bool> DeleteById(string id);
    }
}
