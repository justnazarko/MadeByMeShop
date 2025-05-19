using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadeByMe.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletePostsOnUserDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_SellerId",
                table: "Posts");

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
                name: "FK_Posts_AspNetUsers_SellerId",
                table: "Posts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_SellerId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "687a14f7-1914-4936-aa85-015274b4abe8", "27249131-afd9-4996-a764-cc93e00a670f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f03ed01f-ff5f-490d-b1f4-e488dbe0cdbb", "d4197079-ce5d-4d77-a204-b09608310031" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2415bfe8-def0-4b87-a0a8-912f3c023ea4", "21872c5e-5ffa-4e15-99a5-b536fecd0716" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 13, 44, 28, 967, DateTimeKind.Utc).AddTicks(7232));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 18, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7157));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 5, 14, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 44, 28, 967, DateTimeKind.Utc).AddTicks(7175));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_SellerId",
                table: "Posts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
