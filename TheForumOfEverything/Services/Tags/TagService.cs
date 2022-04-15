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
                    Text = x.Content,
                })
                .ToListAsync();

            return tags;
        }
        public async Task<TagViewModel> Create(CreateTagViewModel model)
        {
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
                Text = modelText,
            };
            return newTagViewModel;
        }

        public async Task EnsureCreated(string postId, string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(postId) || string.IsNullOrWhiteSpace(postId))
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
            Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return null;
            }

            TagViewModel model = new TagViewModel()
            {
                Id = tag.Id,
                Text = tag.Content,
            };

            return model;
        }

        public async Task<TagViewModel> Edit(TagViewModel model)
        {
            string modelId = model.Id;

            Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == modelId);
            if (tag == null)
            {
                return null;
            }

            tag.Content = model.Text;
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteById(string id)
        {
            Tag tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag == null)
            {
                return false;
            }
            context.Tags.Remove(tag);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
