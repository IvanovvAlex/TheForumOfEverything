using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.TagsTests
{
    public class TagModelTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private string tagId = "4564823c-c289-4f24-940f-a9a24957e29a";

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
                var tag = context.Tags.FirstOrDefault(x => x.Id == tagId);

                tag.Posts = context.Posts.ToList();
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
