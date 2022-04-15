using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Models.Shared
{
    public class ContactViewModel
    {
        [Required]
        public string From { get; set; }
        public string To { get; set; } = "theforumofeverythingemail@gmail.com";

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
