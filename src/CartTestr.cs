using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using MadeByMe.src.Controllers;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class CartControllerTests
    {
        private Mock<CartService> _cartServiceMock;
        private Mock<BuyerCartService> _buyerCartServiceMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private CartController _controller;

        // Допоміжний метод для створення мок-UserManager
        private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object,
                null, null, null, null, null, null, null, null);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange: Ініціалізуємо мок-об’єкти та контролер
            _cartServiceMock = new Mock<CartService>();
            _buyerCartServiceMock = new Mock<BuyerCartService>();
            _userManagerMock = GetUserManagerMock();

            // Налаштовуємо UserManager, щоб завжди повертати "buyer123" як ідентифікатор користувача
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("buyer123");

            _controller = new CartController(_cartServiceMock.Object, _buyerCartServiceMock.Object, _userManagerMock.Object);

            // Створюємо фейкового користувача з NameIdentifier = "buyer123" та встановлюємо ControllerContext
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "buyer123")
            }, "TestAuth"));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Test]
        public void Index_ReturnsViewWithCartViewModel()
        {
            // Arrange: Налаштовуємо сервіс для повернення "CartViewModel" (демонстраційне значення)
            var cartViewModel = "CartViewModel";
            _cartServiceMock.Setup(cs => cs.GetUserCart("buyer123")).Returns(cartViewModel);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert: Переконуємося, що результат не null та модель відповідає очікуваному значенню
            Assert.NotNull(result);
            Assert.AreEqual(cartViewModel, result.Model);
        }

        [Test]
        public void AddToCart_ServiceReturnsFalse_ReturnsBadRequest()
        {
            // Arrange
            var dto = new AddToCartDto();
            _buyerCartServiceMock.Setup(bc => bc.AddToCart("buyer123", dto)).Returns(false);

            // Act
            var result = _controller.AddToCart(dto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual("Помилка при додаванні товару", badRequestResult.Value);
        }

        [Test]
        public void AddToCart_ServiceReturnsTrue_ReturnsRedirectToIndex()
        {
            // Arrange
            var dto = new AddToCartDto();
            _buyerCartServiceMock.Setup(bc => bc.AddToCart("buyer123", dto)).Returns(true);

            // Act
            var result = _controller.AddToCart(dto) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("buyer123", result.RouteValues["buyerId"]);
        }

        [Test]
        public void RemoveFromCart_ServiceReturnsFalse_ReturnsNotFound()
        {
            // Arrange
            int postId = 1;
            _buyerCartServiceMock.Setup(bc => bc.RemoveFromCart("buyer123", postId)).Returns(false);

            // Act
            var result = _controller.RemoveFromCart(postId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void RemoveFromCart_ServiceReturnsTrue_ReturnsRedirectToIndex()
        {
            // Arrange
            int postId = 1;
            _buyerCartServiceMock.Setup(bc => bc.RemoveFromCart("buyer123", postId)).Returns(true);

            // Act
            var result = _controller.RemoveFromCart(postId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("buyer123", result.RouteValues["buyerId"]);
        }

        [Test]
        public void GetTotalPrice_CartEntityIsNull_ReturnsNotFound()
        {
            // Arrange
            _cartServiceMock.Setup(cs => cs.GetUserCartEntity("buyer123")).Returns((Cart)null);

            // Act
            var result = _controller.GetTotalPrice();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetTotalPrice_CartEntityExists_ReturnsCartTotalView()
        {
            // Arrange
            var cartEntity = new Cart { CartId = 1, BuyerCarts = new List<object> { "item" } };
            _cartServiceMock.Setup(cs => cs.GetUserCartEntity("buyer123")).Returns(cartEntity);
            var totalPrice = 100;
            _cartServiceMock.Setup(cs => cs.GetCartTotal(cartEntity.CartId)).Returns(totalPrice);

            // Act
            var result = _controller.GetTotalPrice() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("CartTotal", result.ViewName);
            Assert.AreEqual(totalPrice, result.Model);
        }

        [Test]
        public void Checkout_CartEntityIsNull_ReturnsEmptyCartErrorView()
        {
            // Arrange
            _cartServiceMock.Setup(cs => cs.GetUserCartEntity("buyer123")).Returns((Cart)null);

            // Act
            var result = _controller.Checkout() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("EmptyCartError", result.ViewName);
        }

        [Test]
        public void Checkout_CartEntityEmpty_ReturnsEmptyCartErrorView()
        {
            // Arrange: Створюємо кошик, де список BuyerCarts порожній
            var cartEntity = new Cart { CartId = 1, BuyerCarts = new List<object>() };
            _cartServiceMock.Setup(cs => cs.GetUserCartEntity("buyer123")).Returns(cartEntity);

            // Act
            var result = _controller.Checkout() as ViewResult;

            // Assert: Перевіряємо, що повертається View "EmptyCartError"
            Assert.NotNull(result);
            Assert.AreEqual("EmptyCartError", result.ViewName);
        }

        [Test]
        public void Checkout_CartEntityHasItems_ReturnsCheckoutSuccessViewAndClearsCart()
        {
            // Arrange
            var cartEntity = new Cart { CartId = 1, BuyerCarts = new List<object> { "item" } };
            _cartServiceMock.Setup(cs => cs.GetUserCartEntity("buyer123")).Returns(cartEntity);

            // Act
            var result = _controller.Checkout() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("CheckoutSuccess", result.ViewName);
            _cartServiceMock.Verify(cs => cs.ClearCart(cartEntity.CartId), Times.Once);
        }
    }
}
