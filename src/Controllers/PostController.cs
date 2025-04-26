using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadeByMe.src.Models;

namespace MadeByMe.src.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly ApplicationDbContext _context;

        public PostController(PostService postService, ApplicationDbContext context)
        {
            _postService = postService;
            _context = context;
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

        public IActionResult Details(int id) //після додавання Авторизації відредагувати посилання на кошик, коментарі
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            var categories = _context.Categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.Name
        })
        .ToList();

            ViewBag.Categories = categories;
            return View();
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
                return View(createPostDto);

            _postService.CreatePost(createPostDto);
            
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Seller")]
        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            var updateDto = new UpdatePostDto
            {
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                PhotoLink = post.PhotoLink,
                CategoryId = post.CategoryId
            };

            var categories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            })
        .ToList();

            ViewBag.Categories = categories;

            return View(updateDto);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdatePostDto updatePostDto)
        {
            if (!ModelState.IsValid)
                return View(updatePostDto);

            var updatedPost = _postService.UpdatePost(id, updatePostDto);
            if (updatedPost == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Seller")]
        public IActionResult Delete(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _postService.DeletePost(id);
            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}