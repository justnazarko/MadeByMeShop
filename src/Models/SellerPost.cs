// Models/SellerPost.cs

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.Models
{
	public class SellerPost
	{
		[Key]
		[Column("seller_id")]
		public int SellerId { get; set; }

		[ForeignKey("SellerId")]
		public User Seller { get; set; }

		[Key]
		[Column("post_id")]
		public int PostId { get; set; }

		[ForeignKey("PostId")]
		public Post Post { get; set; }
	}
}
