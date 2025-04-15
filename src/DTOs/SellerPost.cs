using System.ComponentModel.DataAnnotations;

namespace MadeByMe.src.DTOs
{
	public class AssignPostToSellerDto
	{
		[Required]
		public int SellerId { get; set; }

		[Required]
		public int PostId { get; set; }
	}

	public class SellerPostsResponseDto
	{
		public int SellerId { get; set; }
		public string SellerName { get; set; }
		public List<PostResponseDto> Posts { get; set; }
	}
}