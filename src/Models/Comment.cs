using System;
using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.Models
{
	public class Comment
	{
		[Key]
		public int CommentId { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public int PostId { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
