using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My.Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "878b4dde-c1a3-4f5e-a2fc-17558d745beb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9233f60b-0ff0-489e-ba40-645e72b10219");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5347b62d-dec9-459b-a492-d6aedbc2f91a", "c5a3f4d8-50a1-411a-9202-ce5a19aae6ec", "Admin", "ADMIN" },
                    { "6f5ca2c5-781d-460c-aed8-770f250665a6", "47b64c64-24a8-4ae9-8220-5f85da203c38", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5347b62d-dec9-459b-a492-d6aedbc2f91a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f5ca2c5-781d-460c-aed8-770f250665a6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "878b4dde-c1a3-4f5e-a2fc-17558d745beb", null, "Admin", "ADMIN" },
                    { "9233f60b-0ff0-489e-ba40-645e72b10219", null, "User", "USER" }
                });
        }
    }
}
