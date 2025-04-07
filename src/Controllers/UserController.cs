
using Microsoft.AspNetCore.Mvc;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;

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
				return Unauthorized();

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		public IActionResult Profile()
		{
			var user = _userService.GetCurrentUser();
			if (user == null)
				return NotFound();

			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateProfile(UpdateUserDto updateUserDto)
		{
			if (!ModelState.IsValid)
				return View(updateUserDto);

			var updatedUser = _userService.UpdateUser(updateUserDto);
			if (updatedUser == null)
				return NotFound();

			return RedirectToAction(nameof(Profile));
		}
	}
}
