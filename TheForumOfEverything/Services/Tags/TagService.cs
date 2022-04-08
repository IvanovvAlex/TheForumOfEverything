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
                    Text = x.Text,
                })
                .ToListAsync();

            return tags;
        }
        public async Task<TagViewModel> Create(CreateTagViewModel model)
        {
            string modelText = model.Text;

            bool isTagExist = await context.Tags.AnyAsync(x => x.Text == modelText);
            if (isTagExist)
            {
                return null;
            }

            Tag newTag = new Tag(modelText);
            await context.Tags.AddAsync(newTag);
            context.SaveChangesAsync();
            string newTagId = newTag.Id;
            TagViewModel newTagViewModel = new TagViewModel()
            {
                Id = newTagId,
                Text = modelText,
            };
            return newTagViewModel;
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
                Text = tag.Text,
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

            tag.Text = model.Text;
            context.SaveChangesAsync();

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
            context.SaveChangesAsync();
            return true;
        }
    }
}
