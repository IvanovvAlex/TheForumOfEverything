using TheForumOfEverything.Models.Comments;

namespace TheForumOfEverything.Services.Comments
{
    public interface ICommentService
    {
        ICollection<CommentViewModel> GetAll();
        ICollection<CommentViewModel> GetLastNComments(int n);
        Task<CommentViewModel> Create(CreateCommentViewModel model, string userId);
        CommentViewModel GetById(string id);
        CommentViewModel Edit(CommentViewModel model);
        bool DeleteById(string id);
    }
}
