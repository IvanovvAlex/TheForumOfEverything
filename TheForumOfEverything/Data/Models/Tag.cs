using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Tag
    {
        public Tag()
        {

        }
        public Tag(string content)
        {
            this.Content = content;
        }

        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
