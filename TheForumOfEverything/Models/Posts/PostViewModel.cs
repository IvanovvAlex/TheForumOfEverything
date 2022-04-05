using System.ComponentModel.DataAnnotations;
using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Models.Posts
{
    public class PostViewModel
    {

        public string Id { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TimeCreated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImgUrl { get; set; }

        [Required, Display(Name = "Header Image")]
        public IFormFile HeaderImage { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; } 
    }
}
