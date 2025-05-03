using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MadeByMe.src.Models;
using MadeByMe.src.Services;
using MadeByMe.src.DTOs;
using Microsoft.AspNetCore.Identity;

public class CartController : Controller
{
    private readonly CartService _cartService;
    private readonly BuyerCartService _buyerCartService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(CartService cartService, BuyerCartService buyerCartService, UserManager<ApplicationUser> userManager)
    {

        _cartService = cartService;
        _buyerCartService = buyerCartService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var buyerId = _userManager.GetUserId(User);
        var cartViewModel = _cartService.GetUserCart(buyerId);

        return View(cartViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddToCart(AddToCartDto dto)
    {
        var buyerId = _userManager.GetUserId(User);

        var result = _buyerCartService.AddToCart(buyerId, dto);
        if (!result)
        {
            return BadRequest("Помилка при додаванні товару");
        }

        return RedirectToAction("Index", new { buyerId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveFromCart(int postId)
    {
        var buyerId = _userManager.GetUserId(User);

        var result = _buyerCartService.RemoveFromCart(buyerId, postId);
        if (!result)
        {
            return NotFound();
        }

        return RedirectToAction("Index", new { buyerId });
    }


    public IActionResult GetTotalPrice()
    {
        var buyerId = _userManager.GetUserId(User);

        var cart = _cartService.GetUserCartEntity(buyerId);
        if (cart == null)
        {
            return NotFound();
        }

        var total = _cartService.GetCartTotal(cart.CartId);
        return View("CartTotal", total);
    }

    [HttpPost]
    public IActionResult Checkout()
    {
        var buyerId = _userManager.GetUserId(User);

        var cart = _cartService.GetUserCartEntity(buyerId);
        if (cart == null || !cart.BuyerCarts.Any())
        {
            return View("EmptyCartError");
        }

        _cartService.ClearCart(cart.CartId);
        return View("CheckoutSuccess");
    }

   
}