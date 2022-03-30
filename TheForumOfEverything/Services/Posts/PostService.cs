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
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Text = x.Text,
                    TimeCreated = x.TimeCreated,
                    User = x.User,
                })
                .ToHashSet();

            return posts;
        }
        public ICollection<PostViewModel> GetLastNPosts(int n)
        {
            ICollection<PostViewModel> posts = context.Posts
                .Skip(Math.Max(0, context.Posts.Count() - n))
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Text = x.Text,
                    TimeCreated = x.TimeCreated,
                    User = x.User,
                })
                .ToHashSet();
            return posts;
        }
        public PostViewModel Create(CreatePostViewModel model, string userId)
        {
            string modelTitle = model.Title;
            string modelShortDescription = model.ShortDescription;
            string modelText = model.Text;

            bool isPostExist = context.Posts.Any(x => x.Title == modelText);
            if (isPostExist)
            {
                return null;
            }

            Post newPost = new Post()
            {
                Title = modelTitle,
                ShortDescription = modelShortDescription,
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
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Text = post.Text,
                TimeCreated = post.TimeCreated,
                User = post.User,
                UserId = post.UserId
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
