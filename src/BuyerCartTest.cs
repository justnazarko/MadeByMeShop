MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using MadeByMe.src.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class BuyerCartControllerTests
    {
        private Mock<BuyerCartService> _buyerCartServiceMock;
        private BuyerCartController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _buyerCartServiceMock = new Mock<BuyerCartService>();
            _controller = new BuyerCartController(_buyerCartServiceMock.Object);
        }

        [Test]
        public void AddToCart_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var dto = new AddToCartDto(); // Заповніть властивості за потреби
            string buyerId = "buyer123";
            _controller.ModelState.AddModelError("Key", "Model is invalid");

            // Act
            var result = _controller.AddToCart(dto, buyerId);

            // Assert: Очікуємо, що повернеться BadRequestResult
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void AddToCart_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var dto = new AddToCartDto(); // Заповніть властивості за потреби
            string buyerId = "buyer123";
            var serviceResult = "Item added successfully"; // Приклад результату від сервісу

            _buyerCartServiceMock.Setup(s => s.AddToCart(buyerId, dto))
                .Returns(serviceResult);

            // Act
            var result = _controller.AddToCart(dto, buyerId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(serviceResult, result.Value);
        }
    }
} 
