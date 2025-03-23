using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public CartController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpPost("add/{postId}")]
	public async Task<IActionResult> AddPost(int postId)
	{
		var post = await _context.Posts.FindAsync(postId);
		if (post == null)
		{
			return NotFound();
		}

		var cart = await _context.Carts.FirstOrDefaultAsync();
		if (cart == null)
		{
			cart = new Cart();
			_context.Carts.Add(cart);
		}

		cart.Posts.Add(post);
		await _context.SaveChangesAsync();

		return Ok();
	}

	[HttpDelete("remove/{postId}")]
	public async Task<IActionResult> RemovePost(int postId)
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		if (cart == null)
		{
			return NotFound();
		}

		var post = cart.Posts.FirstOrDefault(p => p.PostId == postId);
		if (post == null)
		{
			return NotFound();
		}

		cart.Posts.Remove(post);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpGet("total")]
	public async Task<ActionResult<double>> GetTotalPrice()
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		if (cart == null)
		{
			return NotFound();
		}

		double total = cart.Posts.Sum(p => p.ProductPrice);
		return total;
	}

	[HttpPost("checkout")]
	public async Task<IActionResult> Checkout()
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		if (cart == null || !cart.Posts.Any())
		{
			return BadRequest("Кошик порожній.");
		}

	
		// ...

		cart.Posts.Clear();
		await _context.SaveChangesAsync();

		return Ok("Покупка оформлена успішно.");
	}
}
