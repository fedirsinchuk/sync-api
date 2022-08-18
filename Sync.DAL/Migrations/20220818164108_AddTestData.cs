using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sync.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "Name", "Values" },
                values: new object[,]
                {
                    { 1, "Test 1", " " },
                    { 2, "Test 2", " " },
                    { 3, "Test 3", " " },
                    { 4, "Test 4", " " },
                    { 5, "Test 5", " " },
                    { 6, "Test 6", " " }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
