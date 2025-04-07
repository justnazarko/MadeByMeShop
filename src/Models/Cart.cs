using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.Models
{
	public class Cart
	{
		[Key]
		public int CartId { get; set; }

		[Required]
		public int BuyerId { get; set; }

		public List<Post> Posts { get; set; } = new List<Post>();

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
