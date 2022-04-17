using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.CommentsTests
{
    public class CommentModelTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);
        }
        [Test]
        public void ModelGetSetTests()
        {
            try
            {
                var comment = context.Comments.FirstOrDefault();
                comment.AttachedPost = context.Posts.FirstOrDefault();
                var attachedPost = comment.AttachedPost;
                var user = comment.User;
                var userId = comment.UserId;
                var postId = comment.PostId;
                context.SaveChanges();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
