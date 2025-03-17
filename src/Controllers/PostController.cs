using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
                return View(createPostDto);

            _postService.CreatePost(createPostDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdatePost(int id)
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
                Status = post.Status
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            if (!ModelState.IsValid)
                return View(updatePostDto);

            var updatedPost = _postService.UpdatePost(id, updatePostDto);
            if (updatedPost == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeletePost(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _postService.RemovePost(id);
            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
