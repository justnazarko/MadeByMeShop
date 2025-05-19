using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadeByMe.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 0, "687a14f7-1914-4936-aa85-015274b4abe8", "admin@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/admin.jpg", "27249131-afd9-4996-a764-cc93e00a670f", false, "admin" },
                    { "22222222-2222-2222-2222-222222222222", 0, "f03ed01f-ff5f-490d-b1f4-e488dbe0cdbb", "artist@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/artist.jpg", "d4197079-ce5d-4d77-a204-b09608310031", false, "artist123" },
                    { "33333333-3333-3333-3333-333333333333", 0, "2415bfe8-def0-4b87-a0a8-912f3c023ea4", "customer@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/customer.jpg", "21872c5e-5ffa-4e15-99a5-b536fecd0716", false, "customer1" }
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1,
                column: "BuyerId",
                value: "33333333-3333-3333-3333-333333333333");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2,
                column: "BuyerId",
                value: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 5, 19, 13, 44, 28, 967, DateTimeKind.Utc).AddTicks(7232), "33333333-3333-3333-3333-333333333333" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 5, 18, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7237), "11111111-1111-1111-1111-111111111111" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 5, 19, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7157), "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 5, 14, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7164), "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created_at", "SellerId" },
                values: new object[] { new DateTime(2025, 5, 9, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7175), "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "SellerId",
                value: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "SellerId",
                value: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "SellerId",
                value: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333");

            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1554b2f5-e8fc-4ade-8f8e-a21fd2b2ed81", 0, "0053949c-e777-4dd5-9b78-f354c86c4de4", "admin@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEEZ6hGJ4hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/admin.jpg", "70ce32ff-2423-4b24-a13a-f9ef4aedb8dc", false, "admin" },
                    { "50004389-6c50-4bb0-9873-e47102bf85e7", 0, "51b97d20-c005-4252-8183-f08f144ec97c", "customer@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/customer.jpg", "5bd6abe1-599d-421a-b08d-451e04de2843", false, "customer1" },
                    { "be1b9ebc-532b-451a-82e5-337b86bf7a3c", 0, "b6b71449-43a3-43f7-9307-1c919bfa9434", "artist@example.com", false, false, false, null, null, null, "AQAAAAIAAYagAAAAEFz7Oj7hQz2b6J6B2VZqk1vRkXlY7TJi+W7Xq3X9kKJ9pL3h8pZ1Xy9jW8w1g==", null, false, "/images/artist.jpg", "b2382dcd-8765-4444-b1f6-43f90e13e374", false, "artist123" }
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1,
                column: "BuyerId",
                value: "50004389-6c50-4bb0-9873-e47102bf85e7");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 2,
                column: "BuyerId",
                value: "1554b2f5-e8fc-4ade-8f8e-a21fd2b2ed81");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 4, 28, 20, 42, 28, 721, DateTimeKind.Utc).AddTicks(3675), "50004389-6c50-4bb0-9873-e47102bf85e7" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                columns: new[] { "created_at", "UserId" },
                values: new object[] { new DateTime(2025, 4, 27, 22, 42, 28, 721, DateTimeKind.Utc).AddTicks(3677), "1554b2f5-e8fc-4ade-8f8e-a21fd2b2ed81" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created_at", "PhotoLink", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 28, 22, 42, 28, 721, DateTimeKind.Utc).AddTicks(3622), "/images/earring1.jpg", "be1b9ebc-532b-451a-82e5-337b86bf7a3c" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created_at", "PhotoLink", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 23, 22, 42, 28, 721, DateTimeKind.Utc).AddTicks(3625), "/images/vase.jpg", "be1b9ebc-532b-451a-82e5-337b86bf7a3c" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created_at", "PhotoLink", "SellerId" },
                values: new object[] { new DateTime(2025, 4, 18, 22, 42, 28, 721, DateTimeKind.Utc).AddTicks(3631), "/images/painting.jpg", "be1b9ebc-532b-451a-82e5-337b86bf7a3c" });

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "SellerId",
                value: "be1b9ebc-532b-451a-82e5-337b86bf7a3c");

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "SellerId",
                value: "be1b9ebc-532b-451a-82e5-337b86bf7a3c");

            migrationBuilder.UpdateData(
                table: "SellerPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "SellerId",
                value: "be1b9ebc-532b-451a-82e5-337b86bf7a3c");
        }
    }
}
