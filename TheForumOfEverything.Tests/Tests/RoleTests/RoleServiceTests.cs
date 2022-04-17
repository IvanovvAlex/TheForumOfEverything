using Microsoft.AspNetCore.Identity;
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
using TheForumOfEverything.Services.Roles;
using TheForumOfEverything.Services.Shared;
using TheForumOfEverything.Services.Tags;
using TheForumOfEverything.Tests.Data;

namespace TheForumOfEverything.Tests.Tests.RoleTests
{
    public class RoleServiceTests
    {
        public static DbContextOptions<ApplicationDbContext> dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TheForumOfEverythingTests")
        .Options;


        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IRoleService roleService;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ApplicationDbContext(dbOptions);
            context.Database.EnsureCreated();

            DataSeeder.Seed(context);
            
            roleService = new RoleService(roleManager, userManager);
        }

        [Test]
        public void CreateRoleTest()
        {
            Assert.IsTrue(true);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
