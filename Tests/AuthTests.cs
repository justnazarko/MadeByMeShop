using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MadeByMe.src.Controllers;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;
using System.Threading.Tasks;

namespace MadeByMe.Tests
{
    public class AuthTests
    {
        [Fact]
        public async Task Register_ValidUser_RedirectsToHome()
        {
            // Arrange
            var userManager = MockUserManager();
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            userManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                .ReturnsAsync(IdentityResult.Success);
            var signInManager = MockSignInManager(userManager.Object);
            signInManager.Setup(x => x.SignInAsync(It.IsAny<ApplicationUser>(), false, null)).Returns(Task.CompletedTask);
            var controller = new AccountController(userManager.Object, signInManager.Object, null);
            var dto = new RegisterDto { UserName = "testuser", Email = "test@email.com", Password = "Qwerty123!", PhoneNumber = "+380000000000", ConfirmPassword = "Qwerty123!" };

            // Act
            var result = await controller.Register(dto);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public async Task Login_ValidCredentials_RedirectsToHome()
        {
            // Arrange
            var userManager = MockUserManager();
            var user = new ApplicationUser { Email = "test@email.com" };
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            var signInManager = MockSignInManager(userManager.Object);
            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            signInManager.Setup(x => x.SignInAsync(user, false, null)).Returns(Task.CompletedTask);
            var controller = new AccountController(userManager.Object, signInManager.Object, null);
            var dto = new LoginDto { Email = "test@email.com", Password = "Qwerty123!" };

            // Act
            var result = await controller.Login(dto);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        // Моки для UserManager та SignInManager
        private static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private static Mock<SignInManager<ApplicationUser>> MockSignInManager(UserManager<ApplicationUser> userManager)
        {
            var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            return new Mock<SignInManager<ApplicationUser>>(
                userManager,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);
        }
    }
} 