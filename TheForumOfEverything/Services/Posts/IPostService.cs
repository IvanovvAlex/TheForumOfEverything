using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Services.Posts
{
    public interface IPostService
    {
        ICollection<PostViewModel> GetAll();
        ICollection<PostViewModel> GetLastNPosts(int n);
        PostViewModel Create(CreatePostViewModel model, string userId);
        PostViewModel GetById(string id);
        PostViewModel Edit(PostViewModel model);
        bool DeleteById(string id);
    }
}
