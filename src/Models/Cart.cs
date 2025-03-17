using System.Collections.Generic;

public class Cart
{
	public int CartId { get; set; }

	public List<Post> Posts { get; set; } = new List<Post>(); 
}
