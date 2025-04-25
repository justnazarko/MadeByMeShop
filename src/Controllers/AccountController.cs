using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeByMe.src.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationUserService _ApplicationUserService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ApplicationUserService applicationUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ApplicationUserService = applicationUserService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = new ApplicationUser
            {
                Name = dto.Name,
                EmailAddress = dto.EmailAddress
               
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _signInManager.PasswordSignInAsync(dto.EmailAddress, dto.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Невірна електронна пошта або пароль");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction(nameof(Login));

            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return View("Profile", dto);

            var currentUser = _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction(nameof(Login));

            dto.UserId = currentUser.Id;
            var updatedUser = _ApplicationUserService.UpdateUser(currentUser.Id, dto);

            if (updatedUser == null)
                return NotFound();

            return RedirectToAction(nameof(Profile));
        }



    }

}
