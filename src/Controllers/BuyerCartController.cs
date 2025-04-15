using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MadeByMe.src.Services;
using MadeByMe.src.DTOs;

namespace MadeByMe.src.Controllers
{
	public class BuyerCartController : Controller
	{
		private readonly BuyerCartService _buyerCartService;

		public BuyerCartController(BuyerCartService buyerCartService)
		{
			_buyerCartService = buyerCartService;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddToCart(AddToCartDto addToCartDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var result = _buyerCartService.AddToCart(addToCartDto);
			return Ok(result);
		}
	}
}