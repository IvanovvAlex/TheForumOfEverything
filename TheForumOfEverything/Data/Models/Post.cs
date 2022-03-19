using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Post
    {
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string TimeCreated { get; init; } = DateTime.Now.ToString();

        [Required]
        public string Text { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
