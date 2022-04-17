using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.ApplicationUserModelTests
{
    public class ApplicationUserModelTests
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
                var user = context.ApplicationUser.FirstOrDefault();
                user.Comments = context.Comments.ToList();
                user.Posts = context.Posts.ToList();
                var age = user.Age;
                var comments = user.Comments;
                var posts = user.Posts;
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
