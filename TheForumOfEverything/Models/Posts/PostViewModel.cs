using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Models.Posts
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TimeCreated { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; } 
    }
}
