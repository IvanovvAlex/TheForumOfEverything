﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheForumOfEverything.Models;
using TheForumOfEverything.Models.Categories;
using TheForumOfEverything.Models.Posts;
using TheForumOfEverything.Services.Categories;
using TheForumOfEverything.Services.Posts;

namespace TheForumOfEverything.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, IPostService postService, ICategoryService categoryService)
        {
            _logger = logger;
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ICollection<CategoryViewModel> categories = categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}