using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Users;
using TheForumOfEverything.Services.ApplicationUsers;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.ApplicationUsersTests
{
    public class ApplicationUserServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private IApplicationUserService userService;
        private string userId = "fee859e9-7b26-4cf8-a3c5-7cea60c13101";

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            userService = new ApplicationUserService(context);
        }

        [Test]
        public void GetUserByIdTest()
        {
            var actualUser = userService.GetUser(userId).Result;
            var invalidIdUser = userService.GetUser("invalid guid").Result;
            var nullUser = userService.GetUser(null).Result;

            Assert.IsNotNull(actualUser);
            Assert.IsNull(invalidIdUser);
            Assert.IsNull(nullUser);
        }

        [Test]
        public void GetUserViewModelTest()
        {
            ApplicationUser user = context.ApplicationUser.FirstOrDefault(x => x.Id == userId);

            var model = userService.GetUserViewModel(user).Result;
            var nullModel = userService.GetUserViewModel(null).Result;

            Assert.IsNotNull(model);
            Assert.IsNull(nullModel);
        }

        [Test]
        public void GetUsersTest()
        {
            var expectedCount = context.ApplicationUser.Count();
            var users = userService.GetUsers().Result;

            Assert.IsTrue(users.Count == expectedCount);
        }

        [Test]
        public void SearchTest()
        {
            var searchString = "al"; 
            var expectedCount = context.ApplicationUser.Where(x => x.Email.Contains(searchString)).Count();
            var users = userService.Search(searchString).Result;

            Assert.IsTrue(users.Count == expectedCount);
        }

        [Test]
        public void EditUserTest()
        {
            string expectedName = "Edited Name";
            string expectedSurname = "Edited Surname";
            string expectedAddress = "Edited Address";
            string expectedBio = "Edited Bio";
            DateTime expectedBirthday = DateTime.Now;

            ApplicationUser userBeforeEdit = context.ApplicationUser.FirstOrDefault(x => x.Id == userId);

            UserViewModel model = new UserViewModel()
            {
                Name = expectedName,
                Surname = expectedSurname,
                Address = expectedAddress,
                Bio = expectedBio,
                Birthday = expectedBirthday,
            };

            var editedModel = userService.Edit(userId, model);
            var invalidIdModel = userService.Edit("invalid id", model);
            var nullModel = userService.Edit(userId, null);



            ApplicationUser user = context.ApplicationUser.FirstOrDefault(x => x.Id == userId);

            Assert.IsNull(invalidIdModel.Result);
            Assert.IsNull(nullModel.Result);
            Assert.AreEqual(expectedName, user.Name);
            Assert.AreEqual(expectedSurname, user.Surname);
            Assert.AreEqual(expectedAddress, user.Address);
            Assert.AreEqual(expectedBio, user.Bio);
            Assert.AreEqual(expectedBirthday, user.Birthday);

            user = userBeforeEdit;
            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
