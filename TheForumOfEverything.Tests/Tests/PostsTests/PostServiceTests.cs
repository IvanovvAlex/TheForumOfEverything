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
using TheForumOfEverything.Services.Tags;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.PostServiceTests
{
    public class PostServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private ITagService tagService;
        private IPostService postService;
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

            tagService = new TagService(context);
            postService = new PostService(context, tagService);
        }

        [Test]
        public void GetAllPostsTest()
        {
            var expectedCount = context.Posts.Where(x => x.IsApproved && !x.IsDeleted).Count();

            var result = postService.GetAll().Result;

            Assert.AreEqual(expectedCount, result.Count);
        }

        [Test]
        public void GetLastNPostsTest()
        {
            int N = 3;

            var result = postService.GetLastNPosts(N).Result;

            Assert.AreEqual(N, result.Count());
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
                Tags = "testTag1111 testTag2222"
            };

            string userId = "4f79e2cb-960d-422a-89cf-210269e0237a";

            var result = postService.Create(model, userId).Result;
            var nullModel = postService.Create(null, userId).Result;
            var nullUserId = postService.Create(model, null).Result;

            Assert.IsNotNull(result);
            Assert.IsNull(nullModel);
            Assert.IsNull(nullUserId);
        }

        [Test]
        public void GetPostByIdTest()
        {
            var result = postService.GetById(postId).Result;

            var comments = result.Comments;
            var tags = result.Tags;
            var tagsToString = result.TagsToString;
            var img = result.ImgUrl;
            var isApproved = result.IsApproved;
            var isDeleted = result.IsDeleted;
            var timeCreated = result.TimeCreated;
            var user = result.User;
            var userId = result.UserId;
            result.IsDeleted = true;

            var nullIdResult = postService.GetById(null).Result;

            var invalidIdResult = postService.GetById("invalid id").Result;

            Assert.IsNotNull(result);
            Assert.IsNull(nullIdResult);
            Assert.IsNull(invalidIdResult);
        }

        [Test]
        public void EditPostTest()
        {
            string expectedTitle = "Edited Content";
            int expectedTagsCount = context.Tags.Count();

            PostViewModel model = new PostViewModel()
            {
                Id = editPostId,
                Content = expectedTitle,
                TagsToString = " ",
            };

            PostViewModel invalidIdModel = new PostViewModel()
            {
                Id = "Invalid ID",
                Content = expectedTitle,
            };

            var result = postService.Edit(model).Result;
            var nullModelResult = postService.Edit(null).Result;
            var invalidIdResult = postService.Edit(invalidIdModel).Result;

            Assert.AreEqual(expectedTitle, result.Content);
            Assert.IsNotNull(result);
            Assert.IsNull(nullModelResult);
            Assert.IsNull(invalidIdResult);
        }

        [Test]
        public void DeletePostByIdTest()
        {
            var result = postService.DeleteById(deletePostId).Result;

            var nullResult = postService.DeleteById(null).Result;

            var invalidIdResult = postService.DeleteById("Invalid id").Result;

            var category = context.Categories.FirstOrDefault(x => x.Id == deletePostId);

            Assert.IsNull(category);
            Assert.IsTrue(result);
            Assert.IsFalse(nullResult);
            Assert.IsFalse(invalidIdResult);
        }

        [Test]
        public void ApproveByIdTest()
        {
            var result = postService.ApproveById(postId).Result;

            var nullResult = postService.ApproveById(null).Result;

            var invalidIdResult = postService.ApproveById("Invalid id").Result;


            Assert.IsTrue(result);
            Assert.IsFalse(nullResult);
            Assert.IsFalse(invalidIdResult);
        }

        [Test]
        public void ASearchTest()
        {
            string keyword = "Post";
            var result = postService.Search(keyword).Result;

            var nullResult = postService.Search(null).Result;

            Assert.AreEqual(context.Posts.Count(), result.Count());
            Assert.AreEqual(context.Posts.Count(), nullResult.Count());

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
