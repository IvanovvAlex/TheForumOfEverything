using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Comment
    {
        public Comment()
        {

        }

        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required]
        public Post AttachedPost { get; set; }

        [Required]
        public string TimeCreated { get; init; } = DateTime.Now.ToString();

        [Required]
        public string Text { get; set; }
    }
}
