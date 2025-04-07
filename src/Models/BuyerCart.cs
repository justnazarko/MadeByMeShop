using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeByMe.src.Models
{
	public class BuyerCart
	{
		[Key]
		public int CartItemId { get; set; }

		[Required]
		public int CartId { get; set; }

		[ForeignKey("CartId")]
		public Cart Cart { get; set; }

		[Required]
		public int PostId { get; set; }

		[ForeignKey("PostId")]
		public Post Post { get; set; }

		public int Quantity { get; set; } = 1;
	}
}
