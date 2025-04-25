using MadeByMe.src.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MadeByMe.src.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        // DbSet для кожної моделі
        public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<BuyerCart> BuyerCarts { get; set; }
		public DbSet<SellerPost> SellerPosts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            // Налаштування зв'язків та обмежень

            // Налаштування зв'язків
            modelBuilder.Entity<SellerPost>()
                .HasKey(sp => new { sp.SellerId, sp.PostId });

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // Post: зв'язок з User (Seller)
            modelBuilder.Entity<Post>()
				.HasOne(p => p.Seller)
				.WithMany()
				.HasForeignKey(p => p.SellerId)
				.OnDelete(DeleteBehavior.Restrict);

			// Comment: зв'язок з User та Post
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.User)
				.WithMany()
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Post)
				.WithMany()
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			// Cart: зв'язок з User (Buyer)
			modelBuilder.Entity<Cart>()
				.HasOne(c => c.Buyer)
				.WithMany()
				.HasForeignKey(c => c.BuyerId)
				.OnDelete(DeleteBehavior.SetNull); // BuyerId може бути NULL

            modelBuilder.Entity<BuyerCart>()
                .HasOne(bc => bc.Cart)
                .WithMany(c => c.BuyerCarts)
                .HasForeignKey(bc => bc.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BuyerCart>()
				.HasOne(bc => bc.Post)
				.WithMany()
				.HasForeignKey(bc => bc.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			// Додаткові налаштування, якщо потрібно
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
        }
	}
}