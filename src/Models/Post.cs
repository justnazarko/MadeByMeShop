using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.Models
{
    public class Post
    {
        [Key]
        [Column("post_id")]  // Видалити один з атрибутів Column
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]  // Залишити тільки цей атрибут
        public decimal Price { get; set; }
        public string PhotoLink { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SellerId { get; set; }
        public ApplicationUser Seller { get; set; }

        [Column(TypeName = "numeric(3,2)")]
        public decimal Rating { get; set; } = 0.0m;

        [MaxLength(20)]
        public string Status { get; set; } = "active";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}