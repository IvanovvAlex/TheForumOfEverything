using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Comments;

namespace TheForumOfEverything.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext context;
        public CommentService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ICollection<CommentViewModel> GetAll()
        {
            ICollection<CommentViewModel> comments = context.Comments
                .Select(x => new CommentViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                })
                .ToHashSet();

            return comments;
        }
        public ICollection<CommentViewModel> GetLastNComments(int n)
        {
            ICollection<CommentViewModel> comments = context.Comments
                .Skip(Math.Max(0, context.Comments.Count() - n))
                .Select(x => new CommentViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                })
                .ToHashSet();
            return comments;
        }
        public async Task<CommentViewModel> Create(CreateCommentViewModel model, string userId)
        {
            string modelContent = model.Content;

            bool isCommentExist = context.Comments.Any(x => x.Content == modelContent);
            if (isCommentExist)
            {
                return null;
            }

            Comment newComment = new Comment()
            {
                Content = modelContent,
            };
            context.Comments.Add(newComment);
            context.SaveChanges();


            string newCommentId = newComment.Id;
            CommentViewModel newCommentViewModel = new CommentViewModel()
            {
                Id = newCommentId,
            };
            return newCommentViewModel;
        }

        public CommentViewModel GetById(string id)
        {
            Comment comment = context.Comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }

            CommentViewModel model = new CommentViewModel()
            {
                Id = comment.Id,
                Content = comment.Content,
            };

            return model;
        }

        public CommentViewModel Edit(CommentViewModel model)
        {
            string modelId = model.Id;

            Comment comment = context.Comments
                .FirstOrDefault(x => x.Id == modelId);
            if (comment == null)
            {
                return null;
            }

            comment.Content = model.Content;
            context.SaveChanges();

            return model;
        }

        public bool DeleteById(string id)
        {
            Comment comment = context.Comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
            {
                return false;
            }
            context.Comments.Remove(comment);
            context.SaveChanges();
            return true;
        }
    }
}
