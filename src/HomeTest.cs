using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MadeByMe.src.Controllers;
using MadeByMe.src.Models;
using MadeByMe.src.Services;
using MadeByMe.src.DTOs;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<PostService> _postServiceMock;
        private Mock<ILogger<HomeController>> _loggerMock;
        private const string TraceId = "custom-trace";

        [SetUp]
        public void Setup()
        {
            // ARRANGE
            _loggerMock = new Mock<ILogger<HomeController>>();
            _postServiceMock = new Mock<PostService>();
            _controller = new HomeController(_loggerMock.Object, _postServiceMock.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { TraceIdentifier = TraceId }
            };
        }

        [Test]
        public void Index_ReturnsViewWithPostsList()
        {
            // ARRANGE
            var posts = new List<Post>
            {
                new Post 
                { 
                    Id = 1, 
                    Title = "Test Title", 
                    Description = "Test Description", 
                    Price = 100, 
                    PhotoLink = "link1", 
                    Rating = 5, 
                    Status = "Active", 
                    Category = "TestCategory", 
                    Seller = "TestSeller",
                    CreatedAt = DateTime.UtcNow 
                },
                new Post 
                { 
                    Id = 2, 
                    Title = "Second Title", 
                    Description = "Second Description", 
                    Price = 200, 
                    PhotoLink = "link2", 
                    Rating = 4, 
                    Status = "Inactive", 
                    Category = "OtherCategory", 
                    Seller = "OtherSeller",
                    CreatedAt = DateTime.UtcNow 
                }
            };
            _postServiceMock.Setup(ps => ps.GetAllPosts()).Returns(posts);

            // ACT
            var result = _controller.Index() as ViewResult;
            var model = result?.Model as List<PostResponseDto>;

            // ASSERT
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.AreEqual(posts.Count, model.Count);
        }

        [Test]
        public void Privacy_ReturnsView()
        {
            // ACT
            var result = _controller.Privacy() as ViewResult;
            // ASSERT
            Assert.NotNull(result);
        }

        [Test]
        public void Error_ReturnsViewWithErrorViewModel()
        {
            // ARRANGE
            _controller.HttpContext.TraceIdentifier = TraceId;
            // ACT
            var result = _controller.Error() as ViewResult;
            var model = result?.Model as ErrorViewModel;
            // ASSERT
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.AreEqual(TraceId, model.RequestId);
        }
    }
}
