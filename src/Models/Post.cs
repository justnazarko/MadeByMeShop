using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.Models
{
	public class Post
	{
		[Key]
		public int PostId { get; set; } // Було Id

		[Required]
		[MaxLength(100)]
		public string Name { get; set; } // Було Title

		[Required]
		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }

		[Required]
		public string ProductDescription { get; set; } // Було Description

		[Required]
		public decimal ProductPrice { get; set; } // Було double

		[Required]
		public string PhotoLink { get; set; }

		[Required]
		public double Rating { get; set; } = 0.0;

		[Required]
		[MaxLength(20)]
		public string Status { get; set; } = "active";

		[Required]
		public int SellerId { get; set; }

		[ForeignKey("SellerId")]
		public User Seller { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
