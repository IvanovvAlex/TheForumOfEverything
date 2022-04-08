using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Comments;

namespace TheForumOfEverything.Services.Comments
{
    public interface ICommentService
    {
        Task<ICollection<CommentViewModel>> GetAll();
        Task<ICollection<CommentViewModel>> GetLastNComments(int n);
        Task<CommentViewModel> Create(CreateCommentViewModel model, ApplicationUser user, string userId, string postId);
        Task<CommentViewModel> GetById(string id);
        Task<CommentViewModel> Edit(CommentViewModel model);
        Task<bool> DeleteById(string id);
    }
}
