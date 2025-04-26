using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;

public class CommentController : Controller
{
	private readonly ApplicationDbContext _context;
	private readonly CommentService _commentService;

    public CommentController(ApplicationDbContext context, CommentService commentService)
	{
		_context = context;
		_commentService = commentService;
	}

	public async Task<IActionResult> Index()
	{
		var comments = await _context.Comments.ToListAsync();
		return View(comments);
	}

	public async Task<IActionResult> Details(int id) // ?
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null) return NotFound();

		return View(comment);
	}

	public IActionResult Create(int postId)
	{
		return View(new Comment { PostId = postId });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(CreateCommentDto dto)
	{
		if (!ModelState.IsValid) return View(dto);

		var comment = _commentService.AddComment(dto);

		return RedirectToAction("Details", "Post", new { id = comment.PostId });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public IActionResult Delete(int id)
	{
        var comment = _commentService.GetCommentById(id);
        if (comment == null)
            return NotFound();

        
        if (User.IsInRole("Admin"))
        {
            _commentService.DeleteComment(id);
            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        var currentUserName = User.Identity.Name;
        if (comment.User.Name == currentUserName)
        {
            _commentService.DeleteComment(id);
            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        return Forbid();

		//if (!_commentService.DeleteComment(id))
		//{
		//	return NotFound();
		//}
		//else
		//{
		//	return RedirectToAction("Index");
		//}

	}
}
