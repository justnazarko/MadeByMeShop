﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.DTOs
{
    public class CreatePostDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(255)]
        public string PhotoLink { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SellerId { get; set; }
    }

    public class UpdatePostDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [MaxLength(255)]
        public string? PhotoLink { get; set; }

        public int? CategoryId { get; set; }
    }

    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoLink { get; set; }
        public decimal Rating { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string SellerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}