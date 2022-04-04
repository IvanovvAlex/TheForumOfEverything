using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;

namespace TheForumOfEverything.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public ICollection<PostViewModel> GetAll()
        {
            ICollection<PostViewModel> posts = context.Posts
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
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
                    Description = x.Description,
                    Content = x.Content,
                    TimeCreated = x.TimeCreated,
                    User = x.User,
                })
                .ToHashSet();
            return posts;
        }
        public async Task<PostViewModel> Create(CreatePostViewModel model, string userId)
        {
            string modelTitle = model.Title;
            string modelShortDescription = model.Description;
            string modelContent = model.Content;

            bool isPostExist = context.Posts.Any(x => x.Title == modelContent);
            if (isPostExist)
            {
                return null;
            }

            Post newPost = new Post()
            {
                Title = modelTitle,
                Description = modelShortDescription,
                Content = modelContent,
                
                UserId = userId
            };
            context.Posts.Add(newPost);
            context.SaveChanges();

            string webRootPath = webHostEnvironment.WebRootPath;
            string pathToImage = $@"{webRootPath}\UserFiles\Posts\{newPost.Id}\HeaderImage.jpg";

            EnsureFolder(pathToImage);

            using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            {
                 await model.HeaderImage.CopyToAsync(fileStream);
            }

            string newPostId = newPost.Id;
            PostViewModel newTagViewModel = new PostViewModel()
            {
                Id = newPostId,
                Content = modelContent,
            };
            return newTagViewModel;
        }

        public PostViewModel GetById(string id)
        {
            Post post = context.Posts.Include(u => u.User).FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return null;
            }

            PostViewModel model = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                TimeCreated = post.TimeCreated,
                User = post.User,
                UserId = post.UserId
            };

            return model;
        }

        public PostViewModel Edit(PostViewModel model)
        {
            string modelId = model.Id;

            Post post = context.Posts
                .FirstOrDefault(x => x.Id == modelId);
            if (post == null)
            {
                return null;
            }

            post.Content = model.Content;
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
        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}
