using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Models.Tags
{
    public class TagViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
