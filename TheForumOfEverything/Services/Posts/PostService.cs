using System.Security.Claims;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        public PostService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ICollection<PostViewModel> GetAll()
        {
            ICollection<PostViewModel> posts = context.Posts
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                })
                .ToHashSet();

            return posts;
        }
        public PostViewModel Create(CreatePostViewModel model, string userId)
        {
            string modelText = model.Text;

            bool isPostExist = context.Posts.Any(x => x.Text == modelText);
            if (isPostExist)
            {
                return null;
            }

            Post newPost = new Post()
            {
                Text = modelText,
                UserId = userId
            };
            context.Posts.Add(newPost);
            context.SaveChanges();
            string newPostId = newPost.Id;
            PostViewModel newTagViewModel = new PostViewModel()
            {
                Id = newPostId,
                Text = modelText,
            };
            return newTagViewModel;
        }

        public PostViewModel GetById(string id)
        {
            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return null;
            }

            PostViewModel model = new PostViewModel()
            {
                Id = post.Id,
                Text = post.Text,
            };

            return model;
        }

        public PostViewModel Edit(PostViewModel model)
        {
            string modelId = model.Id;

            Post post = context.Posts.FirstOrDefault(x => x.Id == modelId);
            if (post == null)
            {
                return null;
            }

            post.Text = model.Text;
            context.SaveChanges();

            return model;
        }

        public bool DeleteById(string id)
        {
            Post post = context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return false;
            }
            context.Posts.Remove(post);
            context.SaveChanges();
            return true;
        }
    }
}
