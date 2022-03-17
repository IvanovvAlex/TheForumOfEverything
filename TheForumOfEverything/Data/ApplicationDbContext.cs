using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data.Models;

namespace TheForumOfEverything.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAccount>().HasMany(x => x.Posts).WithOne(x => x.User).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserAccount>().HasMany(x => x.Comments).WithOne(x => x.User).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>().HasMany(x => x.Comments).WithOne(x => x.AttachedPost).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Post>().HasMany(x => x.Tags).WithMany(x => x.Posts);

        }
    }
}