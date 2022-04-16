using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Address { get; set; }

        public DateTime Birthday { get; set; } = new DateTime(2000, 01, 01);

        public int Age => (DateTime.Now - Birthday).Days / 365;

        public string? Bio { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
