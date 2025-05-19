using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadeByMe.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteSellerPostsOnUserDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2ea18cab-b023-4749-ae68-d41e7e00a3f3", "2afb0416-f9f3-4c84-b7f3-91724c6718c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aa40349c-38db-47e3-9a69-b96689dc2aaa", "75950f50-c380-4e6a-9d24-474d70364449" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e2b1eac8-56f0-43ac-92ff-ca00756f62c3", "a4396be8-e420-4299-bce3-e07cd6995408" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 14, 13, 7, 294, DateTimeKind.Utc).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 18, 16, 13, 7, 294, DateTimeKind.Utc).AddTicks(2897));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 16, 13, 7, 294, DateTimeKind.Utc).AddTicks(2682));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 14, 16, 13, 7, 294, DateTimeKind.Utc).AddTicks(2688));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 16, 13, 7, 294, DateTimeKind.Utc).AddTicks(2721));

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e5425173-5b69-4a7c-b5cd-5767e0ebd2cc", "00717d49-08fa-4b82-9e5e-1a0801ec8da6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d388274d-ea9e-443e-be28-2186a6d5d453", "1933a346-9106-4dec-a18e-37649f210ed3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2c599d1a-1ba0-4c84-9233-80be84608a50", "caaf9a92-8ce1-4c2a-86e4-f553dafb347c" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 13, 53, 57, 67, DateTimeKind.Utc).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 18, 15, 53, 57, 67, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 15, 53, 57, 67, DateTimeKind.Utc).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 14, 15, 53, 57, 67, DateTimeKind.Utc).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 53, 57, 67, DateTimeKind.Utc).AddTicks(5912));

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPosts_AspNetUsers_SellerId",
                table: "SellerPosts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
