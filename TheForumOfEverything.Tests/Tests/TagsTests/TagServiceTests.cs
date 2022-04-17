using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Models.Tags;
using TheForumOfEverything.Services.Posts;
using TheForumOfEverything.Services.Tags;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.TagsTests
{
    public class TagServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private ITagService tagService;

        private string postId = "98d09cfe-ecd6-48ed-b9e7-3607f26a6a6c";
        private string tagId = "4564823c-c289-4f24-940f-a9a24957e29a";
        private string deleteTagId = "57925fc4-4b92-4617-9470-ffa86d1e9695";

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            tagService = new TagService(context);
        }

        [Test]
        public void GetAllTagsTest()
        {
            var expectedCount = context.Tags.Count();

            var result = tagService.GetAll();

            Assert.AreEqual(expectedCount, result.Result.Count);
        }

        [Test]
        public void CreateTest()
        {
            CreateTagViewModel model = new CreateTagViewModel()
            {
                Text = "TestFreeTag"
            };

            var goodResult = tagService.Create(model).Result;
            var existingResult = tagService.Create(model).Result;
            var nullResult = tagService.Create(null).Result;

            Assert.IsNotNull(goodResult);
            Assert.IsNull(existingResult);
            Assert.IsNull(nullResult);
        }

        [Test]
        public void EnsureCreatedTest()
        {
            try
            {
                tagService.EnsureCreated(postId, "add new tags ala bala");
                tagService.EnsureCreated(null, "add new tags ala bala");
                tagService.EnsureCreated(postId, null);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }


        }

        [Test]
        public void GetByIdTest()
        {
            var result = tagService.GetById(tagId).Result;

            var posts = result.Posts;

            var nullIdResult = tagService.GetById(null).Result;

            var invalidIdResult = tagService.GetById("invalid id").Result;

            Assert.IsNotNull(result);
            Assert.IsNull(nullIdResult);
            Assert.IsNull(invalidIdResult);
        }

        [Test]
        public void EditTest()
        {
            TagViewModel model = new TagViewModel()
            {
                Id = tagId,
                Content = "Test123211"
            };

            var result = tagService.Edit(model).Result;

            var nullModelResult = tagService.Edit(null).Result;

            model.Id = "invalid id";
            var invalidIdResult = tagService.Edit(model).Result;

            model.Id = null;
            var nullIdResult = tagService.Edit(model).Result;


            Assert.IsNotNull(result);
            Assert.IsNull(nullIdResult);
            Assert.IsNull(invalidIdResult);
            Assert.IsNull(nullModelResult);
        }

        [Test]
        public void DeleteByIdTest()
        {
            var result = tagService.DeleteById(deleteTagId).Result;

            var nullIdResult = tagService.DeleteById(null).Result;

            var invalidIdResult = tagService.DeleteById("invalid id").Result;

            Assert.IsTrue(result);
            Assert.IsFalse(nullIdResult);
            Assert.IsFalse(invalidIdResult);
        }

        [Test]
        public void GetMostPopularNTagsTest()
        {
            int N = 3;

            var result = tagService.GetMostPopularNTags(N).Result;

            Assert.AreEqual(N, result.Count());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
