using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class Tag
    {
        public Tag()
        {

        }
        public Tag(string text)
        {
            this.Text = text;
        }

        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Text { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
