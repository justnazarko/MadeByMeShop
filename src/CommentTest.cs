using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using MadeByMe.src.Controllers;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTests
    {
        private Mock<ApplicationDbContext> _contextMock;
        private Mock<CommentService> _commentServiceMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private CommentController _controller;

      
        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSet;
        }

        
        private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object,
                null, null, null, null, null, null, null, null);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange: ініціалізуємо мок-об'єкти
            _contextMock = new Mock<ApplicationDbContext>();
            _commentServiceMock = new Mock<CommentService>();
            _userManagerMock = GetUserManagerMock();
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("user1");

            // Створюємо простого фейкового користувача та встановлюємо ControllerContext
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user1"),
                new Claim(ClaimTypes.Name, "testUser")
            }, "TestAuth"));
            _controller = new CommentController(_contextMock.Object, _commentServiceMock.Object, _userManagerMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };
        }

        [Test]
        public async Task Index_ReturnsViewWithComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1 },
                new Comment { Id = 2 }
            };
            var mockSet = GetQueryableMockDbSet(comments);
            _contextMock.Setup(c => c.Comments).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            CollectionAssert.AreEqual(comments, (result.Model as IEnumerable<Comment>)?.ToList());
        }

        [Test]
        public async Task Details_ExistingComment_ReturnsViewWithComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, PostId = 10 };
            var mockSet = GetQueryableMockDbSet(new List<Comment> { comment });
            _contextMock.Setup(c => c.Comments).Returns(mockSet.Object);

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(comment, result.Model);
        }

        [Test]
        public void Create_InvalidModel_ReturnsViewWithDto()
        {
            // Arrange
            var dto = new CreateCommentDto();
            _controller.ModelState.AddModelError("Error", "Invalid");

            // Act
            var result = _controller.Create(dto) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
        }

        [Test]
        public void Create_ValidModel_RedirectsToPostDetails()
        {
            // Arrange
            var dto = new CreateCommentDto();
            var returnedComment = new Comment { PostId = 42 };
            _commentServiceMock.Setup(s => s.AddComment(dto, "user1")).Returns(returnedComment);

            // Act
            var result = _controller.Create(dto) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Details", result.ActionName);
            Assert.AreEqual("Post", result.ControllerName);
            Assert.AreEqual(42, result.RouteValues["id"]);
        }
    }
}
