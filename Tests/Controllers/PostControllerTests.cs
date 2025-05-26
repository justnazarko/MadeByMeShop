using Xunit;
using Moq;
using MadeByMe.src.Controllers;
using MadeByMe.src.Services;
using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MadeByMe.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly Mock<PostService> _mockPostService;
        private readonly Mock<CommentService> _mockCommentService;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<PhotoService> _mockPhotoService;
        private readonly PostController _controller;

        public PostControllerTests()
        {
            _mockPostService = new Mock<PostService>();
            _mockCommentService = new Mock<CommentService>();
            _mockContext = new Mock<ApplicationDbContext>();
            
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            
            _mockPhotoService = new Mock<PhotoService>();

            _controller = new PostController(
                _mockPostService.Object,
                _mockCommentService.Object,
                _mockContext.Object,
                _mockUserManager.Object,
                _mockPhotoService.Object);
        }

        [Fact]
        public void Index_ReturnsViewWithPosts()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Title = "Test Post",
                    Description = "Test Description",
                    Price = 100.00m,
                    Photos = new List<Photo>(),
                    Rating = 4.5m,
                    Status = "active",
                    Category = "Test Category",
                    Seller = "Test Seller",
                    CreatedAt = DateTime.UtcNow
                }
            };

            _mockPostService.Setup(s => s.SearchPosts(It.IsAny<string>()))
                .Returns(posts);

            // Act
            var result = _controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PostResponseDto>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Details_ExistingPost_ReturnsViewWithPost()
        {
            // Arrange
            var post = new Post
            {
                Id = 1,
                Title = "Test Post",
                Description = "Test Description"
            };

            var comments = new List<Comment>();

            _mockPostService.Setup(s => s.GetPostById(1))
                .Returns(post);
            _mockCommentService.Setup(s => s.GetCommentsForPost(1))
                .Returns(comments);

            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public void Details_NonExistingPost_ReturnsNotFound()
        {
            // Arrange
            _mockPostService.Setup(s => s.GetPostById(999))
                .Returns((Post)null);

            // Act
            var result = _controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ValidPost_RedirectsToIndex()
        {
            // Arrange
            var createDto = new CreatePostDto
            {
                Title = "New Post",
                Description = "New Description",
                Price = 100.00m,
                CategoryId = 1
            };

            var userId = "test-user-id";
            var newPost = new Post { Id = 1 };

            _mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);
            _mockPostService.Setup(s => s.CreatePost(createDto, userId))
                .Returns(newPost);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var createDto = new CreatePostDto();
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(createDto, viewResult.Model);
        }

        [Fact]
        public async Task Edit_ValidPost_RedirectsToIndex()
        {
            // Arrange
            var postId = 1;
            var userId = "test-user-id";
            var updateDto = new UpdatePostDto
            {
                Title = "Updated Post",
                Description = "Updated Description",
                Price = 150.00m,
                CategoryId = 1
            };

            var existingPost = new Post
            {
                Id = postId,
                SellerId = userId,
                Photos = new List<Photo>()
            };

            _mockPostService.Setup(s => s.GetPostById(postId))
                .Returns(existingPost);
            _mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);
            _mockPostService.Setup(s => s.UpdatePost(postId, updateDto))
                .Returns(existingPost);

            // Act
            var result = await _controller.Edit(postId, updateDto);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Delete_ValidPost_RedirectsToIndex()
        {
            // Arrange
            var postId = 1;
            var userId = "test-user-id";
            var post = new Post
            {
                Id = postId,
                SellerId = userId,
                Photos = new List<Photo>()
            };

            _mockPostService.Setup(s => s.GetPostById(postId))
                .Returns(post);
            _mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);
            _mockPostService.Setup(s => s.DeletePost(postId))
                .Returns(true);

            // Act
            var result = await _controller.DeleteConfirmed(postId);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Delete_NonExistingPost_ReturnsNotFound()
        {
            // Arrange
            _mockPostService.Setup(s => s.GetPostById(999))
                .Returns((Post)null);

            // Act
            var result = await _controller.DeleteConfirmed(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_UnauthorizedUser_ReturnsForbid()
        {
            // Arrange
            var postId = 1;
            var userId = "test-user-id";
            var differentUserId = "different-user-id";
            var post = new Post
            {
                Id = postId,
                SellerId = userId
            };

            _mockPostService.Setup(s => s.GetPostById(postId))
                .Returns(post);
            _mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(differentUserId);

            // Act
            var result = await _controller.DeleteConfirmed(postId);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }
    }
} 