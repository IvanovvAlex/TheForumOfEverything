using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Comments;
using TheForumOfEverything.Services.Comments;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.CommentsTests
{
    public class CommentServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private CommentService commentService;
        private string commentId = "f87591df-741d-4cc0-9682-b166ae4564e7";
        private string deleteCommentId = "e1bea40d-4962-48f9-9fb3-d2f6d94e31f3";

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            commentService = new CommentService(context);
        }

        [Test]
        public void GetAllCommentsTest()
        {
            var expectedCount = context.Comments.Count();

            var result = commentService.GetAll();

            Assert.AreEqual(expectedCount, result.Result.Count);
        }

        [Test]
        public void GetLastNCommentsTest()
        {
            int N = 5;

            var result = commentService.GetLastNComments(N);

            Assert.AreEqual(N, result.Result.Count);
        }

        [Test]
        public void CreateCommentTest()
        {
            CreateCommentViewModel model = new CreateCommentViewModel()
            {
                Content = "Test Comment Craete",
            };

            string userId = "4f79e2cb-960d-422a-89cf-210269e0237a";
            string postId = "f701c052-f786-4afb-9798-a1854c5e7437";
            ApplicationUser user = context.ApplicationUser.FirstOrDefault(x => x.Id == userId);
           

            var result = commentService.Create(model, user, userId, postId);
            var nullModel = commentService.Create(null, user, userId, postId);
            var nullUser = commentService.Create(model, null, userId, postId);
            var nullUserId = commentService.Create(model, user, null, postId);
            var nullPostId = commentService.Create(model, user, userId, null);

            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullModel.Result);
            Assert.IsNull(nullUser.Result);
            Assert.IsNull(nullUserId.Result);
            Assert.IsNull(nullPostId.Result);
        }

        [Test]
        public void GetCommentByIdTest()
        {
            var result = commentService.GetById(commentId);

            var nullIdResult = commentService.GetById(null);

            var invalidIdResult = commentService.GetById("invalid id");

            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullIdResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void EditCommentTest()
        {
            string expectedTitle = "Edited Content";

            CommentViewModel model = new CommentViewModel()
            {
                Id = commentId,
                Content = expectedTitle,
            };

            CommentViewModel invalidIdModel = new CommentViewModel()
            {
                Id = "Invalid ID",
                Content = expectedTitle,
            };

            var result = commentService.Edit(model);

            var nullModelResult = commentService.Edit(null);

            var invalidIdResult = commentService.Edit(invalidIdModel);

            Assert.AreEqual(expectedTitle, result.Result.Content);
            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullModelResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void DeleteCategoryByIdTest()
        {
            var result = commentService.DeleteById(deleteCommentId);

            var nullResult = commentService.DeleteById(null);

            var invalidIdResult = commentService.DeleteById("Invalid id");

            var category = context.Categories.FirstOrDefault(x => x.Id == deleteCommentId);

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
