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
        public PostService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public PostService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<ICollection<PostViewModel>> GetAll()
        {
            ICollection<PostViewModel> posts = await context.Posts
                .Include(x => x.User)
                .Where(x => x.IsApproved)
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    TimeCreated = x.TimeCreated,
                    User = x.User,
                })
                .OrderByDescending(x => x.TimeCreated)
                .ToListAsync();

            return posts;
        }

        public async Task<ICollection<Post>> GetAllFromCategory(string categoryId)
        {
            ICollection<Post> posts = await context.Posts
                .Include(x => x.User)
                .Where(x => x.IsApproved && x.CategoryId == categoryId)
                .OrderByDescending(x => x.TimeCreated)
                .ToListAsync();

            return posts;
        }
        public async Task<ICollection<PostViewModel>> GetLastNPosts(int n)
        {
            ICollection<PostViewModel> posts = await context.Posts
                .Where(x => x.IsApproved)
                .OrderByDescending(x => x.TimeCreated)
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    TimeCreated = x.TimeCreated,
                    User = x.User,
                })
                .Take(n)
                .ToListAsync();
            return posts;
        }
        public async Task<PostViewModel> Create(CreatePostViewModel model, string userId)
        {
            if(model == null || userId == null)
            {
                return null;
            }
            string modelTitle = model.Title;
            string modelShortDescription = model.Description;
            string modelContent = model.Content;

            bool isPostExist = await context.Posts.AnyAsync(x => x.Title == modelContent);
            if (isPostExist)
            {
                return null;
            }

            Post newPost = new Post()
            {
                Title = modelTitle,
                Description = modelShortDescription,
                Content = modelContent,
                CategoryId = model.CategoryId,
                UserId = userId
            };
            await context.Posts.AddAsync(newPost);
            await context.SaveChangesAsync();

            //string webRootPath = webHostEnvironment.WebRootPath;
            //string pathToImage = $@"{webRootPath}\UserFiles\Posts\{newPost.Id}\HeaderImage.jpg";

            //EnsureFolder(pathToImage);

            //using (var fileStream = new FileStream(pathToImage, FileMode.Create))
            //{
            //     await model.HeaderImage.CopyToAsync(fileStream);
            //}

            string newPostId = newPost.Id;
            PostViewModel newTagViewModel = new PostViewModel()
            {
                Id = newPostId,
                Content = modelContent,
            };
            return newTagViewModel;
        }

        public async Task<PostViewModel> GetById(string id)
        {
            if (id == null)
            {
                return null;
            }
            Post post = await context.Posts.Include(u => u.User).Include(c => c.Category).Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
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
                UserId = post.UserId,
                Category = post.Category,
                CategoryId = post.CategoryId,
                IsApproved = post.IsApproved,
                Comments = post.Comments.OrderByDescending(x => x.TimeCreated).ToList(),
                ImgUrl = "/assets/img/it-category.jpg"
            };

            return model;
        }

        public async Task<PostViewModel> Edit(PostViewModel model)
        {
            if(model == null)
            {
                return null;
            }
            string modelId = model.Id;

            Post post = await context.Posts
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (post == null)
            {
                return null;
            }

            post.CategoryId = model.CategoryId;
            post.Category = model.Category;
            post.Title = model.Title;
            post.Description = model.Description;
            post.Content = model.Content;
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteById(string id)
        {
            if (id == null)
            {
                return false;
            }
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return false;
            }
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ApproveById(string id)
        {
            if (id == null)
            {
                return false;
            }
            Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return false;
            }
            post.IsApproved = true;
            await context.SaveChangesAsync();
            return true;
        }

       
        //private void EnsureFolder(string path)
        //{
        //    string directoryName = Path.GetDirectoryName(path);
        //    if (directoryName.Length > 0)
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(path));
        //    }
        //}
    }
}
