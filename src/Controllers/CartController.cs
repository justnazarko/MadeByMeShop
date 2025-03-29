using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class CartController : Controller
{
	private readonly ApplicationDbContext _context;

	public CartController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpPost]
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

		return RedirectToAction("Index"); // або іншу відповідну дію
	}

	[HttpPost]
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

		return RedirectToAction("Index"); // або іншу відповідну дію
	}

	public async Task<IActionResult> GetTotalPrice()
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		if (cart == null)
		{
			return NotFound();
		}

		double total = cart.Posts.Sum(p => p.ProductPrice);

		// Повертаємо View з моделлю
		return View("CartTotal", total);
	}

	[HttpPost]
	public async Task<IActionResult> Checkout()
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		if (cart == null || !cart.Posts.Any())
		{
			return View("EmptyCartError");
		}

		// Логіка оформлення покупки...

		cart.Posts.Clear();
		await _context.SaveChangesAsync();

		return View("CheckoutSuccess");
	}

	// Додатковий метод для відображення кошика
	public async Task<IActionResult> Index()
	{
		var cart = await _context.Carts
			.Include(c => c.Posts)
			.FirstOrDefaultAsync();

		return View(cart);
	}
}