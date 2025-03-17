using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.DTOs
{
    public class UpdatePostDto
    {
        //[Required]
        //public int PostId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public double Price { get; set; }

        [Url]
        public string PhotoLink { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        //public int CategoryId { get; set; }
    }
}
