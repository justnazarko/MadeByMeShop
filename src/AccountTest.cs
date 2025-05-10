using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using MadeByMe.src.Services;
using MadeByMe.src.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock = null!;
        private Mock<SignInManager<ApplicationUser>> _signInManagerMock = null!;
        private Mock<ApplicationUserService> _applicationUserServiceMock = null!;
        private AccountController _controller = null!;

        // Допоміжний метод для створення мок-UserManager
        private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        // Допоміжний метод для створення мок-SignInManager
        private static Mock<SignInManager<ApplicationUser>> GetSignInManagerMock(UserManager<ApplicationUser> userManager)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(a => a.HttpContext).Returns(new DefaultHttpContext());
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(o => o.Value).Returns(new IdentityOptions());
            var logger = new Mock<ILogger<SignInManager<ApplicationUser>>>();
            var schemes = new Mock<IAuthenticationSchemeProvider>();
            var confirmation = new Mock<IUserConfirmation<ApplicationUser>>();
            return new Mock<SignInManager<ApplicationUser>>(
                userManager,
                contextAccessor.Object,
                claimsFactory.Object,
                options.Object,
                logger.Object,
                schemes.Object,
                confirmation.Object);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange: Ініціалізуємо мок-об’єкти для залежностей
            _userManagerMock = GetUserManagerMock();
            _signInManagerMock = GetSignInManagerMock(_userManagerMock.Object);
            _applicationUserServiceMock = new Mock<ApplicationUserService>();
            _controller = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, _applicationUserServiceMock.Object);
        }

        #region Register

        [Test]
        public void Register_Get_ReturnsView()
        {
            // Arrange

            // Act: Викликаємо GET дію Register
            var result = _controller.Register() as ViewResult;

            // Assert: Перевіряємо, що повернувся ViewResult
            Assert.NotNull(result);
        }

        [Test]
        public async Task Register_Post_InvalidModel_ReturnsViewWithDto()
        {
            // Arrange: Додаємо помилку в ModelState
            _controller.ModelState.AddModelError("Error", "Invalid");
            var dto = new RegisterDto
            {
                UserName = "test",
                Email = "test@example.com",
                PhoneNumber = "123",
                Password = "pass"
            };

            // Act: Викликаємо POST Register
            var result = await _controller.Register(dto) as ViewResult;

            // Assert: Повертається ViewResult з переданим DTO
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
        }

        [Test]
        public async Task Register_Post_CreationFails_ReturnsViewWithErrors()
        {
            // Arrange
            var dto = new RegisterDto
            {
                UserName = "test",
                Email = "test@example.com",
                PhoneNumber = "123",
                Password = "pass"
            };

            // Симулюємо невдале створення користувача
            _userManagerMock
                .Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Create error" }));

            // Act
            var result = await _controller.Register(dto) as ViewResult;

            // Assert: Повертається ViewResult з тим самим DTO та ModelState містить помилки
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        [Test]
        public async Task Register_Post_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var dto = new RegisterDto
            {
                UserName = "test",
                Email = "test@example.com",
                PhoneNumber = "123",
                Password = "pass"
            };
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _userManagerMock
                .Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);
            _signInManagerMock
                .Setup(m => m.SignInAsync(It.IsAny<ApplicationUser>(), false, null))
                .Returns(Task.CompletedTask);
            _userManagerMock
                .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(dto) as RedirectToActionResult;

            // Assert: Перевіряємо редірект на Home/Index
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        #endregion

        #region Login

        [Test]
        public void Login_Get_ReturnsView()
        {
            // Arrange

            // Act: Викликаємо GET Login
            var result = _controller.Login() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Login_Post_InvalidModel_ReturnsViewWithDto()
        {
            // Arrange: Додаємо помилку в ModelState
            _controller.ModelState.AddModelError("Error", "Invalid");
            var dto = new LoginDto { Email = "test@example.com", Password = "pass" };

            // Act
            var result = await _controller.Login(dto) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
        }

        [Test]
        public async Task Login_Post_UserNotFound_ReturnsViewWithError()
        {
            // Arrange
            var dto = new LoginDto { Email = "nouser@example.com", Password = "pass" };
            _userManagerMock
                .Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Login(dto) as ViewResult;

            // Assert: Повертається ViewResult з доданою помилкою в ModelState
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        [Test]
        public async Task Login_Post_ValidCredentials_RedirectsToHomeIndex()
        {
            // Arrange
            var dto = new LoginDto { Email = "test@example.com", Password = "pass" };
            var user = new ApplicationUser { Id = "1", Email = dto.Email, UserName = "test" };

            _userManagerMock
                .Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);
            _signInManagerMock
                .Setup(m => m.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(SignInResult.Success);
            _signInManagerMock
                .Setup(m => m.SignInAsync(user, false, null))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Login(dto) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [Test]
        public async Task Login_Post_InvalidPassword_ReturnsViewWithError()
        {
            // Arrange
            var dto = new LoginDto { Email = "test@example.com", Password = "wrong" };
            var user = new ApplicationUser { Id = "1", Email = dto.Email, UserName = "test" };

            _userManagerMock
                .Setup(m => m.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);
            _signInManagerMock
                .Setup(m => m.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _controller.Login(dto) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
            Assert.IsFalse(_controller.ModelState.IsValid);
        }

        #endregion

        #region Logout

        [Test]
        public async Task Logout_Post_RedirectsToHomeIndex()
        {
            // Arrange
            _signInManagerMock.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        #endregion

        #region Profile & EditProfile

        [Test]
        public async Task Profile_UserFound_ReturnsViewWithUser()
        {
            // Arrange
            var user = new ApplicationUser { Id = "1", Email = "test@example.com", UserName = "test" };
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var userPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            _userManagerMock.Setup(m => m.GetUserId(userPrincipal)).Returns("1");
            _userManagerMock.Setup(m => m.FindByIdAsync("1")).ReturnsAsync(user);

            // Act
            var result = await _controller.Profile() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user, result.Model);
        }

        [Test]
        public async Task Profile_UserNotFound_RedirectsToLogin()
        {
            // Arrange
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var userPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            _userManagerMock.Setup(m => m.GetUserId(userPrincipal)).Returns("1");
            _userManagerMock.Setup(m => m.FindByIdAsync("1")).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Profile();

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectResult.ActionName);
        }

        [Test]
        public async Task EditProfile_Get_UserFound_ReturnsViewWithDto()
        {
            // Arrange
            var user = new ApplicationUser { Id = "1", Email = "test@example.com", UserName = "test", PhoneNumber = "123" };
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var userPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            _userManagerMock.Setup(m => m.GetUserAsync(userPrincipal)).ReturnsAsync(user);

            // Act
            var result = await _controller.EditProfile() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as UpdateProfileDto;
            Assert.NotNull(model);
            Assert.AreEqual(user.Id, model.UserId);
            Assert.AreEqual(user.Email, model.Email);
        }

        [Test]
        public async Task EditProfile_Post_InvalidModel_ReturnsViewWithDto()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Invalid");
            var dto = new UpdateProfileDto
            {
                UserId = "1",
                Email = "test@example.com",
                UserName = "test",
                PhoneNumber = "123"
            };

            // Act
            var result = await _controller.EditProfile(dto) as ViewResult;

            // Assert: Оскільки ModelState не валідна, очікуємо повернення вью "EditProfile" з dto
            Assert.NotNull(result);
            Assert.AreEqual("EditProfile", result.ViewName);
            Assert.AreEqual(dto, result.Model);
