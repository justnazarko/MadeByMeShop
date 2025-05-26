//using Xunit;
//using Moq;
//using MadeByMe.src.Controllers;
//using MadeByMe.src.Services;
//using MadeByMe.src.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using MadeByMe.src.Models;

//namespace MadeByMe.Tests.Controllers
//{
//    public class CartControllerTests
//    {
//        private readonly Mock<BuyerCartService> _mockBuyerCartService;
//        private readonly Mock<CartService> _mockCartService;
//        private readonly BuyerCartController _controller;

//        public CartControllerTests()
//        {
//            _mockBuyerCartService = new Mock<BuyerCartService>();
//            _mockCartService = new Mock<CartService>();
//            _controller = new BuyerCartController(_mockBuyerCartService.Object);
//        }

//        [Fact]
//        public void AddToCart_ValidModel_ReturnsOkResult()
//        {
//            // Arrange
//            var addToCartDto = new AddToCartDto
//            {
//                PostId = 1,
//                Quantity = 2
//            };
//            var buyerId = "test-user-id";

//            _mockBuyerCartService.Setup(s => s.AddToCart(buyerId, addToCartDto))
//                .Returns(true);

//            // Act
//            var result = _controller.AddToCart(addToCartDto, buyerId);

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.True((bool)okResult.Value);
//        }

//        [Fact]
//        public void AddToCart_InvalidModel_ReturnsBadRequest()
//        {
//            // Arrange
//            var addToCartDto = new AddToCartDto(); // Invalid model
//            var buyerId = "test-user-id";
//            _controller.ModelState.AddModelError("PostId", "Required");

//            // Act
//            var result = _controller.AddToCart(addToCartDto, buyerId);

//            // Assert
//            Assert.IsType<BadRequestResult>(result);
//        }

//        [Fact]
//        public void AddToCart_ServiceThrowsException_ReturnsBadRequest()
//        {
//            // Arrange
//            var addToCartDto = new AddToCartDto
//            {
//                PostId = 1,
//                Quantity = 2
//            };
//            var buyerId = "test-user-id";

//            _mockBuyerCartService.Setup(s => s.AddToCart(buyerId, addToCartDto))
//                .Throws(new ArgumentException("Post not found"));

//            // Act
//            var result = _controller.AddToCart(addToCartDto, buyerId);

//            // Assert
//            Assert.IsType<BadRequestResult>(result);
//        }
//    }
//} 