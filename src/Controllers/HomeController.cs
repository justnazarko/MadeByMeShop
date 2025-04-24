using Microsoft.AspNetCore.Mvc;
using MadeByMe.src.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MadeByMe.src.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;

        public HomeController(ILogger<HomeController> logger, PostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts();

            var postsList = posts.Select(post => new DTOs.PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                PhotoLink = post.PhotoLink,
                Rating = post.Rating,
                Status = post.Status,
                CategoryName = post.Category,
                SellerName = post.Seller,
                CreatedAt = post.CreatedAt
            }).ToList();

            return View(postsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
