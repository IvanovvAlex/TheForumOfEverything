using System.ComponentModel.DataAnnotations;

namespace TheForumOfEverything.Models.Posts
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }

        //[ Display(Name = "Header Image")]
        //public IFormFile HeaderImage { get; set; }
    }
}
