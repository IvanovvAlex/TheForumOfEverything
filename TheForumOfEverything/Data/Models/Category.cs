using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Category
    {

        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
