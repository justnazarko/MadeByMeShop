using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadeByMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_AspNetUsers_seller_id",
                table: "SellerPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_Posts_post_id",
                table: "SellerPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPosts",
                table: "SellerPosts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId1",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "405d063f-dd2b-488d-96cc-6223872acbcc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bd901b6c-742d-46ed-b25c-c99fe709d9b8");

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "post_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "post_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "post_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20f5c6fc-02ea-495a-b367-c894ae104b73");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "seller_id",
                table: "SellerPosts",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "SellerPosts",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPosts_seller_id",
                table: "SellerPosts",
                newName: "IX_SellerPosts_SellerId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SellerPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPosts",
                table: "SellerPosts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailAddress", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "MobileNumber", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "698c6ac5-2ce9-481a-9a28-43d4a2d60de7", 0, "a65a895c-e9a1-4244-8926-c03ebb9c3420", null, "artist@example.com", false, false, false, null, null, "artist123", null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/artist.jpg", "ea516e89-486f-45db-8f62-f3d2cbac485f", false, null },
                    { "793f4a21-e802-4e55-8281-3dae3dfe873f", 0, "dc2271c9-35e8-4760-997e-a7bd6b68a5eb", null, "admin@example.com", false, false, false, null, null, "admin", null, null, "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/admin.jpg", "dbad69e6-06d8-43eb-8c4b-da5a3d18ecda", false, null },
                    { "9721b75d-f5b3-4a0c-a594-53f8c36499c0", 0, "9b1e170e-1e52-4b84-882c-02179015d58f", null, "customer@example.com", false, false, false, null, null, "customer1", null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/customer.jpg", "176ab820-f682-48c8-84d3-8e43c5dbca05", false, null }
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1,
                column: "BuyerId",
                value: "9721b75d-f5b3-4a0c-a594-53f8c36499c0");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2,
                column: "BuyerId",
                value: "793f4a21-e802-4e55-8281-3dae3dfe873f");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 4, 26, 16, 29, 7, 385, DateTimeKind.Utc).AddTicks(4662), "9721b75d-f5b3-4a0c-a594-53f8c36499c0" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 4, 25, 18, 29, 7, 385, DateTimeKind.Utc).AddTicks(4666), "793f4a21-e802-4e55-8281-3dae3dfe873f" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 26, 18, 29, 7, 385, DateTimeKind.Utc).AddTicks(4615), "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 21, 18, 29, 7, 385, DateTimeKind.Utc).AddTicks(4619), "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 16, 18, 29, 7, 385, DateTimeKind.Utc).AddTicks(4630), "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" });

            migrationBuilder.InsertData(
                table: "SellerPosts",
                columns: new[] { "Id", "PostId", "SellerId" },
                values: new object[,]
                {
                    { 1, 1, "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" },
                    { 2, 2, "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" },
                    { 3, 3, "698c6ac5-2ce9-481a-9a28-43d4a2d60de7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellerPosts_PostId",
                table: "SellerPosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_Posts_PostId",
                table: "SellerPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_Posts_PostId",
                table: "SellerPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPosts",
                table: "SellerPosts");

            migrationBuilder.DropIndex(
                name: "IX_SellerPosts_PostId",
                table: "SellerPosts");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "793f4a21-e802-4e55-8281-3dae3dfe873f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9721b75d-f5b3-4a0c-a594-53f8c36499c0");

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "698c6ac5-2ce9-481a-9a28-43d4a2d60de7");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SellerPosts");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "SellerPosts",
                newName: "seller_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "SellerPosts",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPosts_SellerId",
                table: "SellerPosts",
                newName: "IX_SellerPosts_seller_id");

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPosts",
                table: "SellerPosts",
                column: "post_id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailAddress", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "MobileNumber", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "20f5c6fc-02ea-495a-b367-c894ae104b73", 0, "883adbb4-212f-4e49-88f5-6fb4ae56c484", null, "artist@example.com", false, false, false, null, null, "artist123", null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/artist.jpg", "58079de6-5079-46e9-9bbd-641046758313", false, null },
                    { "405d063f-dd2b-488d-96cc-6223872acbcc", 0, "f3d80ce3-9a33-48dc-8493-77a1f646bff4", null, "customer@example.com", false, false, false, null, null, "customer1", null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/customer.jpg", "84b316b6-1f81-4f6c-b22c-9ace3302d48b", false, null },
                    { "bd901b6c-742d-46ed-b25c-c99fe709d9b8", 0, "56c477e2-9b74-4d53-8fd6-ae22caba69d3", null, "admin@example.com", false, false, false, null, null, "admin", null, null, "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, null, false, "/images/admin.jpg", "bdfc89d7-5612-4325-afe6-e4a9323423fa", false, null }
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1,
                column: "BuyerId",
                value: "405d063f-dd2b-488d-96cc-6223872acbcc");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2,
                column: "BuyerId",
                value: "bd901b6c-742d-46ed-b25c-c99fe709d9b8");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                columns: new[] { "created_at", "PostId1", "UserId" },
                values: new object[] { new DateTime(2025, 4, 26, 15, 42, 48, 351, DateTimeKind.Utc).AddTicks(4451), null, "405d063f-dd2b-488d-96cc-6223872acbcc" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                columns: new[] { "created_at", "PostId1", "UserId" },
                values: new object[] { new DateTime(2025, 4, 25, 17, 42, 48, 351, DateTimeKind.Utc).AddTicks(4456), null, "bd901b6c-742d-46ed-b25c-c99fe709d9b8" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 26, 17, 42, 48, 351, DateTimeKind.Utc).AddTicks(4410), "20f5c6fc-02ea-495a-b367-c894ae104b73" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 21, 17, 42, 48, 351, DateTimeKind.Utc).AddTicks(4414), "20f5c6fc-02ea-495a-b367-c894ae104b73" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 16, 17, 42, 48, 351, DateTimeKind.Utc).AddTicks(4423), "20f5c6fc-02ea-495a-b367-c894ae104b73" });

            migrationBuilder.InsertData(
                table: "SellerPosts",
                columns: new[] { "post_id", "seller_id" },
                values: new object[,]
                {
                    { 1, "20f5c6fc-02ea-495a-b367-c894ae104b73" },
                    { 2, "20f5c6fc-02ea-495a-b367-c894ae104b73" },
                    { 3, "20f5c6fc-02ea-495a-b367-c894ae104b73" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId1",
                table: "Comments",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId1",
                table: "Comments",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_AspNetUsers_seller_id",
                table: "SellerPosts",
                column: "seller_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_Posts_post_id",
                table: "SellerPosts",
                column: "post_id",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
