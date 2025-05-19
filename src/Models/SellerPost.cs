// Models/SellerPost.cs

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.Models
{
	public class SellerPost
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string SellerId { get; set; } 

        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; }

        [Required]
        public int PostId { get; set; } 

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
