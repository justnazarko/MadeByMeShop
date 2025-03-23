using System.ComponentModel.DataAnnotations;

public class Comment
{
	[Key]
	public int CommentId { get; set; } 

	[Required]
	public int UserId { get; set; } 

	[Required]
	public int PostId { get; set; } 

	[Required]
	public string Content { get; set; } 
}
