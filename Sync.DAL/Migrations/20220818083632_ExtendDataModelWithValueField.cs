using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sync.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ExtendDataModelWithValueField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "Actions");
        }
    }
}
