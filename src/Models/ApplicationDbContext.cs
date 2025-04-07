using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Models
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		   : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<BuyerCart> BuyerCarts { get; set; }
		public DbSet<SellerPost> SellerPosts { get; set; }
	}
}
