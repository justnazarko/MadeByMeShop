using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class CommentController : Controller
{
	private readonly ApplicationDbContext _context;

	public CommentController(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Details(int id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null)
		{
			return NotFound();
		}
		return View(comment);
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null)
		{
			return NotFound();
		}

		_context.Comments.Remove(comment);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index"); // або іншу відповідну дію
	}

	// Додаткові методи для роботи з коментарями
	public async Task<IActionResult> Index()
	{
		var comments = await _context.Comments.ToListAsync();
		return View(comments);
	}
}