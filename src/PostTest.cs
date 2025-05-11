using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using MadeByMe.src.Controllers;
using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using MadeByMe.src.Services;
using MadeByMe.src.ViewModels;

namespace MadeByMe.Tests.Controllers
{
    [TestFixture]
    public class PostControllerTests
    {
        private Mock<PostService> _postServiceMock;
        private Mock<CommentService> _commentServiceMock;
        private Mock<ApplicationDbContext> _contextMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private PostController _controller;

        // Допоміжний метод для створення мокованого DbSet
        private static Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return dbSet;
        }

        // Допоміжний метод для створення мокованого UserManager
        private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [SetUp]
        public void Setup()
        {
            // ARRANGE
            _postServiceMock = new Mock<PostService>();
            _commentServiceMock = new Mock<CommentService>();
            _contextMock = new Mock<ApplicationDbContext>();
            _userManagerMock = GetUserManagerMock();
            _userManagerMock.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("seller1");

            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "seller1")
            }, "TestAuth"));

            _controller = new PostController(_postServiceMock.Object, _commentServiceMock.Object, _contextMock.Object, _userManagerMock.Object);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Test]
        public void Index_ReturnsViewWithPosts()
        {
            // ARRANGE
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Title1", Description = "Desc1", Price = 100, PhotoLink = "link1", Rating = 5, Status = "Active", Category = "Cat1", Seller = "Seller1", CreatedAt = DateTime.UtcNow },
                new Post { Id = 2, Title = "Title2", Description = "Desc2", Price = 200, PhotoLink = "link2", Rating = 4, Status = "Inactive", Category = "Cat2", Seller = "Seller2", CreatedAt = DateTime.UtcNow }
            };
            _postServiceMock.Setup(s => s.GetAllPosts()).Returns(posts);

            // ACT
            var result = _controller.Index() as ViewResult;
            var model = result?.Model as List<PostResponseDto>;

            // ASSERT
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void Details_ReturnsNotFound_WhenPostIsNull()
        {
            // ARRANGE
            _postServiceMock.Setup(s => s.GetPostById(It.IsAny<int>())).Returns((Post)null);

            // ACT
            var result = _controller.Details(1);

            // ASSERT
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Details_ReturnsView_WithViewModel()
        {
            // ARRANGE
            var post = new Post { Id = 1, Title = "Test", CreatedAt = DateTime.UtcNow };
            var comments = new List<Comment> { new Comment { Id = 10, PostId = 1 } };
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns(post);
            _commentServiceMock.Setup(s => s.GetCommentsForPost(1)).Returns(comments);

            // ACT
            var result = _controller.Details(1) as ViewResult;
            var vm = result?.Model as PostDetailsViewModel;

            // ASSERT
            Assert.NotNull(vm);
            Assert.AreEqual(post, vm.Post);
            CollectionAssert.AreEqual(comments, vm.CommentsList);
        }

        [Test]
        public void CreateGet_ReturnsViewWithCategoryList()
        {
            // ARRANGE
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Cat1" }
            };
            _contextMock.Setup(c => c.Categories).Returns(GetMockDbSet(categories).Object);

            // ACT
            var result = _controller.Create() as ViewResult;
            var categoryList = result?.ViewBag.Categories as List<SelectListItem>;

            // ASSERT
            Assert.NotNull(result);
            Assert.NotNull(categoryList);
            Assert.AreEqual(1, categoryList.Count);
        }

        [Test]
        public void CreatePost_InvalidModel_ReturnsViewWithDto()
        {
            // ARRANGE
            _controller.ModelState.AddModelError("Error", "Invalid");
            var dto = new CreatePostDto { Title = "Test" };

            // ACT
            var result = _controller.Create(dto) as ViewResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
        }

        [Test]
        public void CreatePost_ValidModel_RedirectsToIndex()
        {
            // ARRANGE
            var dto = new CreatePostDto { Title = "Test" };
            _postServiceMock.Setup(s => s.CreatePost(dto, "seller1"));

            // ACT
            var result = _controller.Create(dto) as RedirectToActionResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void EditGet_ReturnsNotFound_WhenPostIsNull()
        {
            // ARRANGE
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns((Post)null);

            // ACT
            var result = _controller.Edit(1);

            // ASSERT
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EditGet_ReturnsForbid_WhenUserNotOwner()
        {
            // ARRANGE
            var post = new Post { Id = 1, SellerId = "otherUser" };
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns(post);

            // ACT
            var result = _controller.Edit(1);

            // ASSERT
            Assert.IsInstanceOf<ForbidResult>(result);
        }

        [Test]
        public void EditGet_ReturnsViewWithUpdateDto()
        {
            // ARRANGE
            var post = new Post { Id = 1, SellerId = "seller1", Title = "Old Title", Description = "Old Desc", Price = 100, PhotoLink = "link", CategoryId = 2 };
            var categories = new List<Category> { new Category { CategoryId = 2, Name = "Cat2" } };
            _contextMock.Setup(c => c.Categories).Returns(GetMockDbSet(categories).Object);
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns(post);

            // ACT
            var result = _controller.Edit(1) as ViewResult;
            var dto = result?.Model as UpdatePostDto;

            // ASSERT
            Assert.NotNull(dto);
            Assert.AreEqual(post.Title, dto.Title);
        }

        [Test]
        public void EditPost_InvalidModel_ReturnsViewWithDto()
        {
            // ARRANGE
            _controller.ModelState.AddModelError("Error", "Invalid");
            var dto = new UpdatePostDto { Title = "New Title" };

            // ACT
            var result = _controller.Edit(1, dto) as ViewResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual(dto, result.Model);
        }

        [Test]
        public void EditPost_UpdateFails_ReturnsNotFound()
        {
            // ARRANGE
            var dto = new UpdatePostDto { Title = "New Title" };
            _postServiceMock.Setup(s => s.UpdatePost(1, dto)).Returns((Post)null);

            // ACT
            var result = _controller.Edit(1, dto);

            // ASSERT
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EditPost_Valid_RedirectsToIndex()
        {
            // ARRANGE
            var dto = new UpdatePostDto { Title = "New Title" };
            var post = new Post { Id = 1, SellerId = "seller1" };
            _postServiceMock.Setup(s => s.UpdatePost(1, dto)).Returns(post);

            // ACT
            var result = _controller.Edit(1, dto) as RedirectToActionResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public void DeleteGet_ReturnsNotFound_WhenPostIsNull()
        {
            // ARRANGE
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns((Post)null);

            // ACT
            var result = _controller.Delete(1);

            // ASSERT
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteGet_ReturnsForbid_WhenUserNotOwner()
        {
            // ARRANGE
            var post = new Post { Id = 1, SellerId = "otherUser" };
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns(post);

            // ACT
            var result = _controller.Delete(1);

            // ASSERT
            Assert.IsInstanceOf<ForbidResult>(result);
        }

        [Test]
        public void DeleteGet_ReturnsViewWithPost()
        {
            // ARRANGE
            var post = new Post { Id = 1, SellerId = "seller1" };
            _postServiceMock.Setup(s => s.GetPostById(1)).Returns(post);

            // ACT
            var result = _controller.Delete(1) as ViewResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual(post, result.Model);
        }

        [Test]
        public void DeleteConfirmed_ReturnsNotFound_WhenDeletionFails()
        {
            // ARRANGE
            _postServiceMock.Setup(s => s.DeletePost(1)).Returns(false);

            // ACT
            var result = _controller.DeleteConfirmed(1);

            // ASSERT
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteConfirmed_Valid_RedirectsToIndex()
        {
            // ARRANGE
            _postServiceMock.Setup(s => s.DeletePost(1)).Returns(true);

            // ACT
            var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}
