using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MadeByMe.src.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
