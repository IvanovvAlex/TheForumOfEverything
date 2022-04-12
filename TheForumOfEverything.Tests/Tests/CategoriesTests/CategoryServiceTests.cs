using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Categories;
using TheForumOfEverything.Services.Categories;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.CategoriesTests
{
    public class CategoryServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private CategoryService categoryService;
        private string deleteCategoryId = "c096270b-0bb3-45e0-bce3-fac5c3837eba";
        private string categoryId = "4564823c-c289-4f24-940f-a9a24957e29a";

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            categoryService = new CategoryService(context);
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            var expectedCount = context.Categories.Count();

            var result = categoryService.GetAll();

            Assert.AreEqual(expectedCount, result.Result.Count);
        }

        [Test]
        public void GetLastNCategoriesTest()
        {
            int N = 5;

            var result = categoryService.GetLastNCategories(N);

            Assert.AreEqual(N, result.Result.Count);
        }

        [Test]
        public void CreateCategoryTest()
        {
            CreateCategoryViewModel model = new CreateCategoryViewModel()
            {
                Title = "Test Category"
            };

            var result = categoryService.Create(model);

            var existingCategoryResult = categoryService.Create(model);

            var nullResult = categoryService.Create(null);

            Assert.IsNotNull(result.Result);
            Assert.IsNull(existingCategoryResult.Result);
            Assert.IsNull(nullResult.Result);
        }

        [Test]
        public void GetCategoryByIdTest()
        {

            var result = categoryService.GetById(categoryId);

            var nullIdResult = categoryService.GetById(null);

            var invalidIdResult = categoryService.GetById("invalid id");

            var resultPosts = result.Result.Posts.ToArray();
            var expectedPosts = context.Categories.FirstOrDefault(x => x.Id == categoryId).Posts
                                                                                          .OrderByDescending(x => x.TimeCreated)
                                                                                          .ToArray();

            Assert.IsTrue(expectedPosts.SequenceEqual(resultPosts));
            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullIdResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void EditCategoryTest()
        {
            string expectedTitle = "Edited Title";

            CategoryViewModel model = new CategoryViewModel()
            {
                Id = categoryId,
                Title = expectedTitle,
            };

            CategoryViewModel invalidIdModel = new CategoryViewModel()
            {
                Id = "Invalid ID",
                Title = expectedTitle,
            };

            var result = categoryService.Edit(model);

            var nullModelResult = categoryService.Edit(null);

            var invalidIdResult = categoryService.Edit(invalidIdModel);

            Assert.AreEqual(expectedTitle, result.Result.Title);
            Assert.IsNotNull(result.Result);
            Assert.IsNull(nullModelResult.Result);
            Assert.IsNull(invalidIdResult.Result);
        }

        [Test]
        public void DeleteCategoryByIdTest()
        {
            var result = categoryService.DeleteById(deleteCategoryId);

            var nullResult = categoryService.DeleteById(null);

            var invalidIdResult = categoryService.DeleteById("Invalid id");

            var category = context.Categories.FirstOrDefault(x => x.Id == deleteCategoryId);

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
