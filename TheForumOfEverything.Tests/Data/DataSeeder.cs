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
                new ApplicationUser
                {
                    Id = "4f79e2cb-960d-422a-89cf-210269e0237a", //6
                    Name = "Milko",
                    Surname="Kostov",
                    Email = "milko@abv.g",
                    Bio = "Milko Mama",
                },
            };

            return users;
        }

        public static ICollection<Tag> Tags()
        {
            List<Tag> tags = new List<Tag>
            {
                new Tag
                {
                   Id = "4564823c-c289-4f24-940f-a9a24957e29a",
                   Content = "Tag1Test",
                },
                new Tag
                {
                   Id = "57925fc4-4b92-4617-9470-ffa86d1e9695",
                   Content = "Tag2Test",
                },
                new Tag
                {
                   Id = "7e3ff432-5219-4dfc-9c3d-391ee800190a",
                   Content = "Tag3Test",
                },
               
            };

            return tags;
        }

        public static ICollection<Comment> Comments()
        {
            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                   Id = "0d4e53e7-fecc-462c-9d76-da5e33ce4548",
                   Content = "Test Comment 1",
                   UserId = "fee859e9-7b26-4cf8-a3c5-7cea60c13101",
                   PostId = "f701c052-f786-4afb-9798-a1854c5e7437",
                },
                new Comment
                {
                   Id = "5b867f15-b340-4d54-9f24-14535bbb9187",
                   Content = "Test Comment 2",
                   UserId = "99c88001-1221-4ff7-a0e9-f5efac98fe9e",
                   PostId = "f701c052-f786-4afb-9798-a1854c5e7437",
                },
                new Comment
                {
                   Id = "f87591df-741d-4cc0-9682-b166ae4564e7",
                   Content = "Test Comment 3",
                   UserId = "71b0265a-1994-4b4a-a824-8a4401aae60e",
                   PostId = "a6891d9a-277d-48b9-b474-8326a1c4d1c4",
                },
                new Comment
                {
                   Id = "e1bea40d-4962-48f9-9fb3-d2f6d94e31f3",
                   Content = "Test Comment 4",
                   UserId = "b9a3f5d8-532d-49eb-b3f8-f0b4e95bb62e",
                   PostId = "0d1a15a4-4592-4e7a-bbc3-86992ee7abfa",
                },
                new Comment
                {
                   Id = "0ebcc842-b724-4c06-bf7f-14515340bd92",
                   Content = "Test Comment 5",
                   UserId = "e3749770-6d4e-4f3c-886b-13e953eafb50",
                   PostId = "31735522-39f9-4286-af7c-81b02aefd547",
                },
                new Comment
                {
                   Id = "9aa0ba4c-d107-4b23-9f17-bf45501c37f2",
                   Content = "Test Comment 6",
                   UserId = "4f79e2cb-960d-422a-89cf-210269e0237a",
                   PostId = "98d09cfe-ecd6-48ed-b9e7-3607f26a6a6c",
                },
            };

            return comments;
        }

        public static ICollection<Post> Posts()
        {
            List<Post> posts = new List<Post>
            {
                new Post
                {
                   Id = "f701c052-f786-4afb-9798-a1854c5e7437",
                   Title = "Post 1",
                   Description = "Post Description 1",
                   Content = "Test Post 1",
                   UserId = "e3749770-6d4e-4f3c-886b-13e953eafb50",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "c096270b-0bb3-45e0-bce3-fac5c3837eba",
                   TagsToString = "ala bala",

                },
                new Post
                {
                   Id = "a6891d9a-277d-48b9-b474-8326a1c4d1c4",
                   Title = "Post 2",
                   Description = "Post Description 2",
                   Content = "Test Post 2",
                   UserId = "b9a3f5d8-532d-49eb-b3f8-f0b4e95bb62e",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "86c84b69-beae-4c62-b8f4-cd4892fded12",
                   TagsToString = "a b",

                },
                new Post
                {
                   Id = "0d1a15a4-4592-4e7a-bbc3-86992ee7abfa",
                   Title = "Post 3",
                   Description = "Post Description 3",
                   Content = "Test Post 3",
                   UserId = "71b0265a-1994-4b4a-a824-8a4401aae60e",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "7e3ff432-5219-4dfc-9c3d-391ee800190a",
                   TagsToString = "testTag1 testTag2",
                },
                new Post
                {
                   Id = "31735522-39f9-4286-af7c-81b02aefd547",
                   Title = "Post 4",
                   Description = "Post Description 4",
                   Content = "Test Post 4",
                   UserId = "99c88001-1221-4ff7-a0e9-f5efac98fe9e",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "57925fc4-4b92-4617-9470-ffa86d1e9695",
                   TagsToString = "tag1",


                },
                new Post
                {
                   Id = "98d09cfe-ecd6-48ed-b9e7-3607f26a6a6c",
                   Title = "Post 5",
                   Description = "Post Description 5",
                   Content = "Test Post 5",
                   UserId = "fee859e9-7b26-4cf8-a3c5-7cea60c13101",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "4564823c-c289-4f24-940f-a9a24957e29a",
                   TagsToString = "22",

                },
                new Post
                {
                   Id = "e12d9700-3441-40ad-8ecb-ff3bd3d058b6",
                   Title = "Post 6",
                   Description = "Post Description 6",
                   Content = "Test Post 6",
                   UserId = "fee859e9-7b26-4cf8-a3c5-7cea60c13101",
                   IsApproved = true,
                   TimeCreated = DateTime.Now.ToString(),
                   CategoryId = "4564823c-c289-4f24-940f-a9a24957e29a",
                   TagsToString = "121",

                },
            };

            return posts;
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
            var tags = Tags();
            var comments = Comments();
            var posts = Posts();

            context.ApplicationUser.AddRange(users);
            context.Categories.AddRange(categories);
            context.Comments.AddRange(comments);
            context.Posts.AddRange(posts);
            context.Tags.AddRange(tags);

            context.SaveChanges();
        }
    }
}
