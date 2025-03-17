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
            if (category == null)
                return NotFound();

            return View(category);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
                return View(createCategoryDto);

            _categoryService.CreateCategory(createCategoryDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            var updateDto = new UpdateCategoryDto
            {
                Name = category.Name
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
                return View(updateCategoryDto);

            var updatedCategory = _categoryService.UpdateCategory(id, updateCategoryDto);
            if (updatedCategory == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _categoryService.RemoveCategory(id);
            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
