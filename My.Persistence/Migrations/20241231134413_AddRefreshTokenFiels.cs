using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFiels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5347b62d-dec9-459b-a492-d6aedbc2f91a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f5ca2c5-781d-460c-aed8-770f250665a6");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87701020-680e-48f6-ad90-8c96c40b6160", "c0f9e775-0264-4108-a550-53f3d43a1948", "User", "USER" },
                    { "93329524-e397-434b-bdf9-b150a72fad76", "0a07bb14-4bd4-453e-b47c-eeb40ca5c89e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87701020-680e-48f6-ad90-8c96c40b6160");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93329524-e397-434b-bdf9-b150a72fad76");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5347b62d-dec9-459b-a492-d6aedbc2f91a", "c5a3f4d8-50a1-411a-9202-ce5a19aae6ec", "Admin", "ADMIN" },
                    { "6f5ca2c5-781d-460c-aed8-770f250665a6", "47b64c64-24a8-4ae9-8220-5f85da203c38", "User", "USER" }
                });
        }
    }
}
