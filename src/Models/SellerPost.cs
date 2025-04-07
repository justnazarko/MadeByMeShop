using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.Models
{
	public class SellerPost
	{
		public int SellerId { get; set; }
		[ForeignKey("SellerId")]
		public User Seller { get; set; }

		public int PostId { get; set; }
		[ForeignKey("PostId")]
		public Post Post { get; set; }
	}
}
