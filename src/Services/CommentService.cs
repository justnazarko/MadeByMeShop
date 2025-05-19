using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MadeByMe.src.Services
{
	public class CommentService
	{
		private readonly ApplicationDbContext _context;

		public CommentService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<Comment> GetCommentsForPost(int postId)
		{
			return _context.Comments
				.Where(c => c.PostId == postId)
				.Include(c => c.User)
				.ToList();
		}

        public Comment GetCommentById(int id)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefault(c => c.CommentId == id);
        }


        public Comment AddComment(CreateCommentDto dto, string userId)
		{
			var comment = new Comment
			{
				UserId = userId,
				PostId = dto.PostId,
				Content = dto.Content
			};

			_context.Comments.Add(comment);
			_context.SaveChanges();
			return comment;
		}

		public bool DeleteComment(int id)
		{
			var comment = _context.Comments.Find(id);
			if (comment != null)
			{
				_context.Comments.Remove(comment);
				_context.SaveChanges();
				return true;
			}
			return false;
		}
	}
}