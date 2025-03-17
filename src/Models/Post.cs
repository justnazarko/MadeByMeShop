using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MadeByMe.src.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        //[Required]
        //public Category Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        //public List<Comment> Comments { get; set; } = new List<Comment>();


    }
}
