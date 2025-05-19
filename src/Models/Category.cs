﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MadeByMe.src.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; } // Було Id

		[Required]
		[MaxLength(50)] // Було 100, в SQL - 50
		public string Name { get; set; }

		public List<Post> Posts { get; set; } = new List<Post>();
	}
}
