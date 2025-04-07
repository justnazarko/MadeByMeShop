using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MadeByMe.src.Models;

public class CommentController : Controller
{
	private readonly ApplicationDbContext _context;

	public CommentController(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{
		var comments = await _context.Comments.ToListAsync();
		return View(comments);
	}

	public async Task<IActionResult> Details(int id)
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
	public async Task<IActionResult> Create(Comment comment)
	{
		if (!ModelState.IsValid) return View(comment);

		_context.Comments.Add(comment);
		await _context.SaveChangesAsync();

		return RedirectToAction("Details", "Post", new { id = comment.PostId });
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null) return NotFound();

		_context.Comments.Remove(comment);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index");
	}
}
