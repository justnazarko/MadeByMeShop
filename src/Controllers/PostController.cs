using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MadeByMe.src.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
                return View(createPostDto);

            _postService.CreatePost(createPostDto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
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

            return View(updateDto);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Delete(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [Authorize]
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