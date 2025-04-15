using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MadeByMe.src.Services
{
	public class CategoryService
	{
		private readonly ApplicationDbContext _context;

		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<Category> GetAllCategories()
		{
			return _context.Categories
				.Include(c => c.Posts)
				.ToList();
		}

		public Category GetCategoryById(int id)
		{
			return _context.Categories
				.Include(c => c.Posts)
				.FirstOrDefault(c => c.CategoryId == id);
		}

		public Category CreateCategory(CreateCategoryDto dto)
		{
			var category = new Category { Name = dto.Name };
			_context.Categories.Add(category);
			_context.SaveChanges();
			return category;
		}

		public Category UpdateCategory(int id, UpdateCategoryDto dto)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				category.Name = dto.Name;
				_context.SaveChanges();
			}
			return category;
		}

		public bool DeleteCategory(int id)
		{
			var category = _context.Categories.Find(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				_context.SaveChanges();
				return true;
			}
			return false;
		}
        public bool RemoveCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}