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

        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Tag> tags { get; set; }

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