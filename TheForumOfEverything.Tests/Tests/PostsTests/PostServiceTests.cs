using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Posts;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.PostsTests
{
    public class PostServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private PostService postService;
        private string postId = "98d09cfe-ecd6-48ed-b9e7-3607f26a6a6c";
        private string deletePostId = "31735522-39f9-4286-af7c-81b02aefd547";
        private string editPostId = "f701c052-f786-4afb-9798-a1854c5e7437";
        private string categoryId = "57925fc4-4b92-4617-9470-ffa86d1e9695";

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            postService = new PostService(context);
        }

        [Test]
        public void GetAllPostsTest()
        {
            var expectedCount = context.Posts.Where(x => x.IsApproved && !x.IsDeleted).Count();

            var result = postService.GetAll();

            Assert.AreEqual(expectedCount, result.Result.Count);
        }

        [Test]
        public void GetLastNPostsTest()
        {
            int N = 3;

            var result = postService.GetLastNPosts(N);

            Assert.AreEqual(N, result.Result.Count()); 
        }

        [Test]
        public void CreatePostTest()
        {
            CreatePostViewModel model = new CreatePostViewModel()
            {
                Title = "Test Post",
                Description = "Test Post Description",
                Content = "Test Post Craete",
                CategoryId = categoryId,
            };

            string userId = "4f79e2cb-960d-422a-89cf-210269e0237a";

            var result = postService.Create(model, userId);
            var nullModel = postService.Create(null,userId);
            var nullUserId = postService.Create(model, null);

            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullModel.Result);
            Assert.IsNull(nullUserId.Result);
        }

        [Test]
        public void GetPostByIdTest()
        {
            var result = postService.GetById(postId);

            var nullIdResult = postService.GetById(null);

            var invalidIdResult = postService.GetById("invalid id");

            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullIdResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void EditPostTest()
        {
            string expectedTitle = "Edited Content";

            PostViewModel model = new PostViewModel()
            {
                Id = editPostId,
                Content = expectedTitle,
            };

            PostViewModel invalidIdModel = new PostViewModel()
            {
                Id = "Invalid ID",
                Content = expectedTitle,
            };

            var result = postService.Edit(model);

            var nullModelResult = postService.Edit(null);

            var invalidIdResult = postService.Edit(invalidIdModel);

            Assert.AreEqual(expectedTitle, result.Result.Content);
            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullModelResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void DeletePostByIdTest()
        {
            var result = postService.DeleteById(deletePostId);

            var nullResult = postService.DeleteById(null);

            var invalidIdResult = postService.DeleteById("Invalid id");

            var category = context.Categories.FirstOrDefault(x => x.Id == deletePostId);

            Assert.IsNull(category);
            Assert.IsTrue(result.Result);
            Assert.IsFalse(nullResult.Result);
            Assert.IsFalse(invalidIdResult.Result);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
