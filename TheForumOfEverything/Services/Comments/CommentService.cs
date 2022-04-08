using Microsoft.EntityFrameworkCore;
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
        public async Task<ICollection<CommentViewModel>> GetAll()
        {
            ICollection<CommentViewModel> comments = await context.Comments
                .Select(x => new CommentViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                })
                .ToListAsync();

            return comments;
        }
        public async Task<ICollection<CommentViewModel>> GetLastNComments(int n)
        {
            ICollection<CommentViewModel> comments = await context.Comments
                .Skip(Math.Max(0, context.Comments.Count() - n))
                .Select(x => new CommentViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                })
                .ToListAsync();
            return comments;
        }
        public async Task<CommentViewModel> Create(CreateCommentViewModel model, ApplicationUser user, string userId, string postId)
        {
            string modelContent = model.Content;

            Comment newComment = new Comment()
            {
                Content = modelContent,
                UserId = userId,
                PostId = postId,
                User = user,
            };
            await context.Comments.AddAsync(newComment);
            context.SaveChangesAsync();


            string newCommentId = newComment.Id;
            CommentViewModel newCommentViewModel = new CommentViewModel()
            {
                Id = newCommentId,
            };
            return newCommentViewModel;
        }

        public async Task<CommentViewModel> GetById(string id)
        {
            Comment comment = await context.Comments.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<CommentViewModel> Edit(CommentViewModel model)
        {
            string modelId = model.Id;

            Comment comment = await context.Comments
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (comment == null)
            {
                return null;
            }

            comment.Content = model.Content;
            context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteById(string id)
        {
            Comment comment = await context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return false;
            }
            context.Comments.Remove(comment);
            context.SaveChangesAsync();
            return true;
        }
    }
}
