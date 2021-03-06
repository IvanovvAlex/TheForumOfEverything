using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Tags;

namespace TheForumOfEverything.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext context;
        public TagService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<TagViewModel>> GetAll()
        {
            ICollection<TagViewModel> tags = await context.Tags
                .Select(x => new TagViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                })
                .ToListAsync();

            return tags;
        }
        public async Task<TagViewModel> Create(CreateTagViewModel model)
        {
            if (model == null)
            {
                return null;
            }
            string modelText = model.Text;

            bool isTagExist = await context.Tags.AnyAsync(x => x.Content == modelText);
            if (isTagExist)
            {
                return null;
            }

            Tag newTag = new Tag(modelText);
            await context.Tags.AddAsync(newTag);
            await context.SaveChangesAsync();
            string newTagId = newTag.Id;
            TagViewModel newTagViewModel = new TagViewModel()
            {
                Id = newTagId,
                Content = modelText,
            };
            return newTagViewModel;
        }

        public async Task EnsureCreated(string postId, string input)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(postId))
            {
                return;
            }
            List<string> listOfTags = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var tagContent in listOfTags)
            {
                bool isTagExist = await context.Tags.AnyAsync(x => x.Content == tagContent);
                if (!isTagExist)
                {
                    Tag newTag = new Tag(tagContent);
                    await context.Tags.AddAsync(newTag);
                    await context.SaveChangesAsync();
                }

                Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Content == tagContent);
                Post post = await context.Posts.FirstOrDefaultAsync(x => x.Id == postId);

                if (post != null)
                {
                    tag.Posts.Add(post);
                    post.Tags.Add(tag);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<TagViewModel> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            Tag tag = await context.Tags.Include(x => x.Posts.Where(x => x.IsApproved && !x.IsDeleted)).FirstOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return null;
            }

            TagViewModel model = new TagViewModel()
            {
                Id = tag.Id,
                Content = tag.Content,
                Posts = tag.Posts
            };

            return model;
        }

        public async Task<TagViewModel> Edit(TagViewModel model)
        {
            if (model == null)
            {
                return null;
            }
            string modelId = model.Id;

            Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == modelId);
            if (tag == null)
            {
                return null;
            }

            tag.Content = model.Content;
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return false;
            }
            context.Tags.Remove(tag);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<TagViewModel>> GetMostPopularNTags(int N)
        {
            if (N == null)
            {
                return null;
            }

            ICollection<TagViewModel> tags = await context.Tags
                            .Include(x => x.Posts.Where(x => x.IsApproved && !x.IsDeleted))
                            .OrderByDescending(x => x.Posts.Count)
                            .Select(x => new TagViewModel()
                            {
                                Id = x.Id,
                                Content = x.Content,
                                Posts = x.Posts
                            })
                            .Take(N)
                            .ToListAsync();
            return tags;
        }
    }
}
