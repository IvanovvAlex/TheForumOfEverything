using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheForumOfEverything.Data;
using TheForumOfEverything.Services.Posts;
using TheForumOfEverything.Services.Tags;
using TheForumOfEverything.Services.ApplicationUsers;
using TheForumOfEverything.Data.Models;
using TheForumOfEverything.Services.Comments;
using TheForumOfEverything.Services.Categories;
using TheForumOfEverything.Services.Roles;
using TheForumOfEverything.Areas.Administration.Services;
using TheForumOfEverything.Areas.Administration.Services.ApplicationUsers;
using TheForumOfEverything.Services.Shared;
using TheForumOfEverything.Areas.Administration.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); 
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<IApplicationUserAdminService, ApplicationUserAdminService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IPostAdminService, PostAdminService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ISharedService, SharedService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Administration",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<ChatHub>("/chat");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

    db.Database.Migrate();
}

app.Run();
