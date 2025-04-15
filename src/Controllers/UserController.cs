using Microsoft.AspNetCore.Mvc;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Authorization;

namespace MadeByMe.src.Controllers
{
	public class UserController : Controller
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(RegisterUserDto registerUserDto)
		{
			if (!ModelState.IsValid)
				return View(registerUserDto);

			_userService.RegisterUser(registerUserDto);
			return RedirectToAction(nameof(Login));
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Login(LoginUserDto loginUserDto)
		{
			if (!ModelState.IsValid)
				return View(loginUserDto);

			var user = _userService.LoginUser(loginUserDto);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid login attempt");
				return View(loginUserDto);
			}

			// Тут має бути логіка автентифікації (SignInManager)
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public IActionResult Profile()
		{
			var user = _userService.GetCurrentUser();
			if (user == null)
				return RedirectToAction(nameof(Login));

			return View(user);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateProfile(UpdateUserDto updateUserDto)
		{
			if (!ModelState.IsValid)
				return View("Profile", updateUserDto);

			var currentUser = _userService.GetCurrentUser();
			if (currentUser == null)
				return RedirectToAction(nameof(Login));

			updateUserDto.UserId = currentUser.UserId;
			var updatedUser = _userService.UpdateUser(currentUser.UserId, updateUserDto);

			if (updatedUser == null)
				return NotFound();

			return RedirectToAction(nameof(Profile));
		}
	}
}