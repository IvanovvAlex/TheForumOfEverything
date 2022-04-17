using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.PostsTests
{
    public class PostModelTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private string postId = "98d09cfe-ecd6-48ed-b9e7-3607f26a6a6c";

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
                Post post = context.Posts.FirstOrDefault(x => x.Id == postId);
                var isDeleted = post.IsDeleted;
                var tagsToString = post.TagsToString;

                post.Comments = context.Comments.ToList();
                post.Tags = context.Tags.ToList();
                post.User = DataSeeder.User();

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
