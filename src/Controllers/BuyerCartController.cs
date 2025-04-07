public class BuyerCartController : Controller
{
	private readonly BuyerCartService _buyerCartService;

	public BuyerCartController(BuyerCartService buyerCartService)
	{
		_buyerCartService = buyerCartService;
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult AddToCart(int postId, int quantity)
	{
		_buyerCartService.AddItemToCart(postId, quantity);
		return RedirectToAction("Index", "Cart");
	}
}