﻿using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;

namespace MadeByMe.src.Data
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. Категорії (2 приклади)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Handmade Jewelry" },
                new Category { CategoryId = 2, Name = "Home Decor" },
                new Category { CategoryId = 3, Name = "Art"}
            );

            // 2. Користувачі (3 приклади)
            var userId1 = "11111111-1111-1111-1111-111111111111";
            var userId2 = "22222222-2222-2222-2222-222222222222";
            var userId3 = "33333333-3333-3333-3333-333333333333";

            // First, update all references to use the new user IDs
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Срібна сережка",
                    Description = "Ручної роботи з натуральним каменем",
                    Price = 799.99m,
                    CategoryId = 1,
                    SellerId = userId2,
                    CreatedAt = DateTime.UtcNow
                },
                new Post
                {
                    Id = 2,
                    Title = "Декоративна ваза",
                    Description = "Керамічна ваза з українським орнаментом",
                    Price = 1200.50m,
                    CategoryId = 2,
                    SellerId = userId2,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Post
                {
                    Id = 3,
                    Title = "Картина 'Сонячний день'",
                    Description = "Олія на полотні, 40x60 см",
                    Price = 2500.00m,
                    CategoryId = 3,
                    SellerId = userId2,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentId = 1,
                    UserId = userId3,
                    PostId = 1,
                    Content = "Дуже гарна сережка! Якісне виконання.",
                    CreatedAt = DateTime.UtcNow.AddHours(-2)
                },
                new Comment
                {
                    CommentId = 2,
                    UserId = userId1,
                    PostId = 3,
                    Content = "Чудова картина, автор - талановитий!",
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            );

            modelBuilder.Entity<Cart>().HasData(
                new Cart
                {
                    CartId = 1,
                    BuyerId = userId3
                },
                new Cart
                {
                    CartId = 2,
                    BuyerId = userId1
                }
            );

            modelBuilder.Entity<BuyerCart>().HasData(
                new BuyerCart
                {
                    CartItemId = 1,
                    CartId = 1,
                    PostId = 1,
                    Quantity = 2
                },
                new BuyerCart
                {
                    CartItemId = 2,
                    CartId = 1,
                    PostId = 3,
                    Quantity = 1
                },
                new BuyerCart
                {
                    CartItemId = 3,
                    CartId = 2,
                    PostId = 2,
                    Quantity = 1
                }
            );

            modelBuilder.Entity<SellerPost>().HasData(
                new SellerPost
                {
                    Id = 1,
                    SellerId = userId2,
                    PostId = 1
                },
                new SellerPost
                {
                    Id = 2,
                    SellerId = userId2,
                    PostId = 2
                },
                new SellerPost
                {
                    Id = 3,
                    SellerId = userId2,
                    PostId = 3
                }
            );

            // Finally, add the users
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = userId1,
                    UserName = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                    ProfilePicture = "/images/admin.jpg"
                },
                new ApplicationUser
                {
                    Id = userId2,
                    UserName = "artist123",
                    Email = "artist@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                    ProfilePicture = "/images/artist.jpg"
                },
                new ApplicationUser
                {
                    Id = userId3,
                    UserName = "customer1",
                    Email = "customer@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==",
                    ProfilePicture = "/images/customer.jpg"
                }
            );
        }
    }
}