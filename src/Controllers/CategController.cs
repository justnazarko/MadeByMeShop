using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadeByMe.src.Controllers
{
	public class CategoryController : Controller
	{
		private readonly CategoryService _categoryService;

		public CategoryController(CategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public IActionResult Index()
		{
			var categories = _categoryService.GetAllCategories();
			return View(categories);
		}

		public IActionResult Details(int id)
		{
			var category = _categoryService.GetCategoryById(id);
			if (category == null) return NotFound();

			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CreateCategoryDto createCategoryDto)
		{
			if (!ModelState.IsValid) return View(createCategoryDto);

			_categoryService.CreateCategory(createCategoryDto);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			var category = _categoryService.GetCategoryById(id);
			if (category == null) return NotFound();

			var updateDto = new UpdateCategoryDto { Name = category.Name, Description = category.Description };
			return View(updateDto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, UpdateCategoryDto updateCategoryDto)
		{
			if (!ModelState.IsValid) return View(updateCategoryDto);

			var updatedCategory = _categoryService.UpdateCategory(id, updateCategoryDto);
			if (updatedCategory == null) return NotFound();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			var category = _categoryService.GetCategoryById(id);
			if (category == null) return NotFound();

			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if (!_categoryService.RemoveCategory(id)) return NotFound();

			return RedirectToAction(nameof(Index));
		}
	}
}
