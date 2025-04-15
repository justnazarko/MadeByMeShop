using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MadeByMe.src.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(int buyerId, int postId)
    {
        var post = await _context.Posts.FindAsync(postId);
        if (post == null) return NotFound();

        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.BuyerId == buyerId);
        if (cart == null)
        {
            cart = new Cart { BuyerId = buyerId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        var cartItem = await _context.BuyerCarts
            .FirstOrDefaultAsync(bc => bc.CartId == cart.CartId && bc.PostId == postId);

        if (cartItem != null)
        {
            cartItem.Quantity += 1;
        }
        else
        {
            _context.BuyerCarts.Add(new BuyerCart
            {
                CartId = cart.CartId,
                PostId = postId,
                Quantity = 1
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", new { buyerId });
    }

    [HttpPost]
    public async Task<IActionResult> RemovePost(int buyerId, int postId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.BuyerId == buyerId);
        if (cart == null) return NotFound();

        var cartItem = await _context.BuyerCarts
            .FirstOrDefaultAsync(bc => bc.CartId == cart.CartId && bc.PostId == postId);

        if (cartItem == null) return NotFound();

        _context.BuyerCarts.Remove(cartItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { buyerId });
    }

    public async Task<IActionResult> GetTotalPrice(int buyerId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.BuyerId == buyerId);
        if (cart == null) return NotFound();

        var total = await _context.BuyerCarts
            .Where(bc => bc.CartId == cart.CartId)
            .SumAsync(bc => bc.Quantity * _context.Posts
                .Where(p => p.Id == bc.PostId)
                .Select(p => p.Price)
                .FirstOrDefault());

        return View("CartTotal", total);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(int buyerId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.BuyerId == buyerId);
        if (cart == null || !await _context.BuyerCarts.AnyAsync(bc => bc.CartId == cart.CartId))
            return View("EmptyCartError");

        _context.BuyerCarts.RemoveRange(_context.BuyerCarts.Where(bc => bc.CartId == cart.CartId));
        await _context.SaveChangesAsync();

        return View("CheckoutSuccess");
    }

    public async Task<IActionResult> Index(int buyerId)
    {
        var cart = await _context.Carts
            .Include(c => c.BuyerCarts)
            .ThenInclude(bc => bc.Post)
            .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

        return View(cart);
    }
}