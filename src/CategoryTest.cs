using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using MadeByMe.src.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;

public class CategoryControllerTests
{
    private readonly Mock<CategoryService> _categoryServiceMock;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _categoryServiceMock = new Mock<CategoryService>();
        _controller = new CategoryController(_categoryServiceMock.Object);
    }

    [Fact]
    public void Index_ReturnsViewWithCategories()
    {
        var categories = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "TestCategory" } };
        _categoryServiceMock.Setup(service => service.GetAllCategories()).Returns(categories);

      
        var result = _controller.Index() as ViewResult;

    
        Assert.NotNull(result);
        Assert.Equal(categories, result.Model);
    }

    [Fact]
    public void Details_CategoryExists_ReturnsView()
    {
   
        var category = new CategoryDto { Id = 1, Name = "TestCategory" };
        _categoryServiceMock.Setup(service => service.GetCategoryById(1)).Returns(category);

    
        var result = _controller.Details(1) as ViewResult;

        Assert.NotNull(result);
        Assert.Equal(category, result.Model);
    }

    [Fact]
    public void Details_CategoryNotFound_ReturnsNotFound()
    {
      
        _categoryServiceMock.Setup(service => service.GetCategoryById(It.IsAny<int>())).Returns((CategoryDto)null);

       
        var result = _controller.Details(1);

    
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_Post_ValidModel_RedirectsToIndex()
    {
    
        var createDto = new CreateCategoryDto { Name = "NewCategory" };

     
        var result = _controller.Create(createDto) as RedirectToActionResult;

       
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }

    [Fact]
    public void Edit_CategoryExists_ReturnsView()
    {
        
        var category = new CategoryDto { Id = 1, Name = "TestCategory" };
        _categoryServiceMock.Setup(service => service.GetCategoryById(1)).Returns(category);

        var result = _controller.Edit(1) as ViewResult;

        Assert.NotNull(result);
        Assert.IsType<UpdateCategoryDto>(result.Model);
    }

    [Fact]
    public void DeleteConfirmed_CategoryExists_RedirectsToIndex()
    {
     
        _categoryServiceMock.Setup(service => service.RemoveCategory(It.IsAny<int>())).Returns(true);

     
        var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;


        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
    }
} 
