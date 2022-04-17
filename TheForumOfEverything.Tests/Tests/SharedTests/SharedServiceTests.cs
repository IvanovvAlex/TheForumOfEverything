using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Models.Shared;
using TheForumOfEverything.Services.Posts;
using TheForumOfEverything.Services.Shared;
using TheForumOfEverything.Services.Tags;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.SharedServiceTests
{
    public class SharedServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private IConfiguration conf;
        private ISharedService sharedService;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);

            sharedService = new SharedService(conf);
        }

        [Test]
        public void SendEmailsTest()
        {
            ContactViewModel model = new ContactViewModel()
            {
                From = "alexivanovv4@gmail.com",
                Name = "Alex",
                PhoneNumber = "09892121",
                Subject = "Test",
                Body = "Body"
            };
            try
            {
                sharedService.EmailSender(model);
                model.To = model.From;
                model.From = GlobalConstants.MainEmail;
                sharedService.EmailSender(model);
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
