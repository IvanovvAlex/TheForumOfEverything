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
        public ICollection<TagViewModel> GetAll()
        {
            ICollection<TagViewModel> tags = context.Tags
                .Select(x => new TagViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                })
                .ToHashSet();

            return tags;
        }
        public TagViewModel Create(CreateTagViewModel model)
        {
            string modelText = model.Text;

            bool isTagExist = context.Tags.Any(x => x.Text == modelText);
            if (isTagExist)
            {
                return null;
            }

            Tag newTag = new Tag(modelText);
            context.Tags.Add(newTag);
            context.SaveChanges();
            string newTagId = newTag.Id;
            TagViewModel newTagViewModel = new TagViewModel()
            {
                Id = newTagId,
                Text = modelText,
            };
            return newTagViewModel;
        }

        public TagViewModel GetById(string id)
        {
            Tag tag = context.Tags.FirstOrDefault(x => x.Id == id);
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

        public TagViewModel Edit(TagViewModel model)
        {
            string modelId = model.Id;

            Tag tag = context.Tags.FirstOrDefault(x => x.Id == modelId);
            if (tag == null)
            {
                return null;
            }

            tag.Text = model.Text;
            context.SaveChanges();

            return model;
        }

        public bool DeleteById(string id)
        {
            Tag tag = context.Tags.FirstOrDefault(x => x.Id == id);
            if (tag == null)
            {
                return false;
            }
            context.Tags.Remove(tag);
            context.SaveChanges();
            return true;
        }
    }
}
