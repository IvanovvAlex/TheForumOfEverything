using Ganss.XSS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Security.Application;
using System.Security.Claims;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Tags;

namespace TheForumOfEverything.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagService tagService;
       
        public PostService(ApplicationDbContext context, ITagService tagService)
        {
            this.context = context;
            this.tagService = tagService;
        }
        public async Task<ICollection<PostViewModel>> GetAll()
        {
            ICollection<PostViewModel> posts = await context.Posts
                .Include(x => x.User)
                .Where(x => x.IsApproved && !x.IsDeleted)
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
                .Where(x => x.IsApproved && !x.IsDeleted && x.CategoryId == categoryId)
                .OrderByDescending(x => x.TimeCreated)
                .ToListAsync();

            return posts;
        }
        public async Task<ICollection<PostViewModel>> GetLastNPosts(int n)
        {
            ICollection<PostViewModel> posts = await context.Posts
                .Where(x => x.IsApproved && !x.IsDeleted)
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
            string modelTags = model.Tags;



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
                UserId = userId,
                TagsToString = modelTags
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
            Post post = await context.Posts.Include(u => u.User).Include(c => c.Category).Include(c => c.Comments).Include(t => t.Tags).FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return null;
            }

            //var sanitizer = new HtmlSanitizer();

            //var sanitized = sanitizer.Sanitize(post.Content, "http://www.example.com");

            List<string> listOfTags = post.Tags.Select(x => x.Content).ToList();

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
                Tags = post.Tags,
                TagsToString = string.Join(' ', listOfTags),
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

            Post post = await context.Posts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (post == null)
            {
                return null;
            }

            List<Tag> tags = post.Tags.ToList();

            foreach (var tag in tags)
            {
                tag.Posts.Remove(post);
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
            post.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ApproveById(string id)
        {
            if (id == null)
            {
                return false;
            }

            Post post = await context.Posts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

            await tagService.EnsureCreated(id, post.TagsToString);

            if (post == null)
            {
                return false;
            }
            post.IsApproved = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<PostViewModel>> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
            {
                return await this.GetAll();
            }
            ICollection<PostViewModel> posts = await context.Posts
                            .Include(x => x.User)
                            .Where(x => x.IsApproved && !x.IsDeleted && x.Title.Contains(searchString))
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
