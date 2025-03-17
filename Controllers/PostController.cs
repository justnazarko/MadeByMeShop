using MadeByMe.DTOs;
using MadeByMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadeByMe.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        
    }
}
