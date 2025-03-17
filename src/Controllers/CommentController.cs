using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public CommentController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Comment>> GetCommentById(int id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null)
		{
			return NotFound();
		}
		return comment;
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCommentById(int id)
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null)
		{
			return NotFound();
		}

		_context.Comments.Remove(comment);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
