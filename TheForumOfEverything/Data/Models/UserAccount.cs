using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class UserAccount
    {
        public UserAccount()
        {

        }

        [Key]
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? City { get; set; }

        public DateTime Birthday { get; set; }

        public int Age => (DateTime.Now - Birthday).Days / 365;

        public string? EmailAddress { get; set; }

        public string? Bio { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
