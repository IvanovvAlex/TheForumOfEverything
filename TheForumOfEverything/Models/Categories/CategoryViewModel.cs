using System.ComponentModel.DataAnnotations;
using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Models.Categories
{
    public class CategoryViewModel
    {
        public string Id { get; init; } 
        public string Title { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
