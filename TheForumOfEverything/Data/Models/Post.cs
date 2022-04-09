using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Post
    {
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string TimeCreated { get; init; } = DateTime.Now.ToString();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
