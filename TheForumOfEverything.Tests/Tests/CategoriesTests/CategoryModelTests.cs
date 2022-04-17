using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.CategoriesTests
{
    public class CategoryModelTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private string categoryId = "57925fc4-4b92-4617-9470-ffa86d1e9695";

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
                var category = context.Categories.FirstOrDefault(x => x.Id == categoryId);
                category.Posts = context.Posts.ToList();
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
