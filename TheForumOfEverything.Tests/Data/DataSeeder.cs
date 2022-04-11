using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForumOfEverything.Data;
using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Tests.Data
{
    internal static class DataSeeder
    {
        public static List<ApplicationUser> Users()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "fee859e9-7b26-4cf8-a3c5-7cea60c13101", //1 
                    Name = "Alex",
                    Surname="Ivanov",
                    Email = "alexivanovv4@gmail.com",
                    Bio = "Junior Full stack Software Developer",
                },
                new ApplicationUser
                {
                    Id = "99c88001-1221-4ff7-a0e9-f5efac98fe9e", //2
                    Name = "Daniel",
                    Surname="Dimitrov",
                    Email = "daniel@dimitrov.com",
                    Bio = "Junior Full stack Software Developer Intern",
                },
                new ApplicationUser
                {
                    Id = "71b0265a-1994-4b4a-a824-8a4401aae60e", //3
                    Name = "Georgi",
                    Surname="Ivanov",
                    Email = "gosho@trucks.com",
                    Bio = "Truck Driver",
                },
                new ApplicationUser
                {
                    Id = "b9a3f5d8-532d-49eb-b3f8-f0b4e95bb62e", //5
                    Name = "Nadya",
                    Surname="Ivanova",
                    Email = "algona@abv.bg",
                    Bio = "Company Owner",
                },
                new ApplicationUser
                {
                    Id = "e3749770-6d4e-4f3c-886b-13e953eafb50", //5
                    Name = "Niki",
                    Surname="Nikov",
                    Email = "ivo@yahh.koc",
                    Bio = "Boring Guy",
                },
            };

            return users;
        }

        public static ICollection<Category> Categories()
        {
            List<Category> categories = new List<Category>
            {
                new Category
                {
                   Id = "4564823c-c289-4f24-940f-a9a24957e29a",
                   Title = "IT",
                },
                new Category
                {
                   Id = "57925fc4-4b92-4617-9470-ffa86d1e9695",
                   Title = "Cars",
                },
                new Category
                {
                   Id = "7e3ff432-5219-4dfc-9c3d-391ee800190a",
                   Title = "Medicine",
                },
                new Category
                {
                   Id = "86c84b69-beae-4c62-b8f4-cd4892fded12",
                   Title = "Kids",
                },
                new Category
                {
                   Id = "c096270b-0bb3-45e0-bce3-fac5c3837eba",
                   Title = "Hunting",
                },
                new Category
                {
                   Id = "60a88faf-865f-43ed-8d3a-51cfc4bbb4c2",
                   Title = "Jobs",
                },
                new Category
                {
                   Id = "b0a6748b-ab28-4757-abd8-e2c7213cca7f",
                   Title = "Jokes",
                },
            };

            return categories;
        }

        public static ApplicationUser User()
        {
            return new ApplicationUser
            {
                Id = "81590b37-a922-4c95-8603-1caa40fba811",
                Email = "user123@abv.bg",
                UserName = "user123@abv.bg",
            };
        }

        public static void Seed(ApplicationDbContext context)
        {
            context.Users.Add(User());

            var users = Users();
            var categories = Categories();

            context.ApplicationUser.AddRange(users);
            context.Categories.AddRange(categories);

            context.SaveChanges();
        }
    }
}
