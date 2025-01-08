using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAndUpdateRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bf7e566-3529-48a5-bf06-3a9d2471aa81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fbc73f4-58cd-401d-852d-aa12cfdb15a0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "878b4dde-c1a3-4f5e-a2fc-17558d745beb", null, "Admin", "ADMIN" },
                    { "9233f60b-0ff0-489e-ba40-645e72b10219", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "1bf7e566-3529-48a5-bf06-3a9d2471aa81", null, "User", "USER" },
                    { "4fbc73f4-58cd-401d-852d-aa12cfdb15a0", null, "Admin", "Admin" }
                });
        }
    }
}
