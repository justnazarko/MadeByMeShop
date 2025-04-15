using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadeByMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Handmade Jewelry" },
                    { 2, "Home Decor" },
                    { 3, "Art" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "EmailAddress", "IsBlocked", "ModelNumber", "Password", "ProfilePicture", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, "admin@example.com", false, null, "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", "/images/admin.jpg", "Admin", "admin" },
                    { 2, "artist@example.com", false, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", "/images/artist.jpg", "Seller", "artist123" },
                    { 3, "customer@example.com", false, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", "/images/customer.jpg", "Buyer", "customer1" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "BuyerId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "post_id", "CategoryId", "created_at", "Description", "PhotoLink", "Price", "Rating", "SellerId", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 4, 7, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3084), "Ручної роботи з натуральним каменем", "/images/earring1.jpg", 799.99m, 0.0m, 2, "active", "Срібна сережка" },
                    { 2, 2, new DateTime(2025, 4, 2, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3085), "Керамічна ваза з українським орнаментом", "/images/vase.jpg", 1200.50m, 0.0m, 2, "active", "Декоративна ваза" },
                    { 3, 3, new DateTime(2025, 3, 28, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3092), "Олія на полотні, 40x60 см", "/images/painting.jpg", 2500.00m, 0.0m, 2, "active", "Картина 'Сонячний день'" }
                });

            migrationBuilder.InsertData(
                table: "BuyerCarts",
                columns: new[] { "CartItemId", "CartId", "PostId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 1, 3, 1 },
                    { 3, 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "created_at", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "Дуже гарна сережка! Якісне виконання.", new DateTime(2025, 4, 7, 19, 45, 14, 107, DateTimeKind.Utc).AddTicks(3116), 1, 3 },
                    { 2, "Чудова картина, автор - талановитий!", new DateTime(2025, 4, 6, 21, 45, 14, 107, DateTimeKind.Utc).AddTicks(3118), 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "SellerPosts",
                columns: new[] { "post_id", "seller_id" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuyerCarts",
                keyColumn: "CartItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BuyerCarts",
                keyColumn: "CartItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BuyerCarts",
                keyColumn: "CartItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumns: new[] { "post_id", "seller_id" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumns: new[] { "post_id", "seller_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumns: new[] { "post_id", "seller_id" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "post_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "post_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "post_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "user_id",
                keyValue: 3);
        }
    }
}
