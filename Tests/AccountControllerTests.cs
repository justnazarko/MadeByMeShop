using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MadeByMe.src.Controllers;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;

namespace MadeByMe.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            // Створюємо моки для UserManager та SignInManager
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null, null, null, null);

            _controller = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, null);
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var model = new RegisterDto
            {
                UserName = "testuser",
                Email = "test@email.com",
                Password = "Test123!",
                ConfirmPassword = "Test123!",
                PhoneNumber = "+380000000000"
            };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Register_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var model = new RegisterDto
            {
                UserName = "testuser",
                Email = "test@email.com",
                Password = "Test123!",
                ConfirmPassword = "WrongPassword",
                PhoneNumber = "+380000000000"
            };

            _controller.ModelState.AddModelError("ConfirmPassword", "Passwords do not match");

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsRedirectToActionResult()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@email.com",
                Password = "Test123!"
            };

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, model.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _signInManagerMock.Setup(x => x.SignInAsync(user, false, null)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsViewResult()
        {
            // Arrange
            var model = new LoginDto
            {
                Email = "test@email.com",
                Password = "WrongPassword"
            };

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            _userManagerMock.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(user);

            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, model.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
            Assert.True(_controller.ModelState.ErrorCount > 0);
        }
    }
} 