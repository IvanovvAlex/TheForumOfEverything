using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Models.Users
{
    public class UserViewModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Address { get; set; }

        public DateTime Birthday { get; set; }

        public int Age => (DateTime.Now - Birthday).Days / 365;

        public string? Bio { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
