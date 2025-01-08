using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenDatatimeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87701020-680e-48f6-ad90-8c96c40b6160");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93329524-e397-434b-bdf9-b150a72fad76");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "669f9304-b607-4a0c-b82a-8bc7751358ba", "fcd8532b-1189-4ea4-a8d7-14edfba28a1e", "Admin", "ADMIN" },
                    { "82494c2f-30e0-4b77-b795-e99a7ee7d913", "8408750b-ea37-4c31-b254-cead1a6c9945", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "669f9304-b607-4a0c-b82a-8bc7751358ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82494c2f-30e0-4b77-b795-e99a7ee7d913");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87701020-680e-48f6-ad90-8c96c40b6160", "c0f9e775-0264-4108-a550-53f3d43a1948", "User", "USER" },
                    { "93329524-e397-434b-bdf9-b150a72fad76", "0a07bb14-4bd4-453e-b47c-eeb40ca5c89e", "Admin", "ADMIN" }
                });
        }
    }
}
