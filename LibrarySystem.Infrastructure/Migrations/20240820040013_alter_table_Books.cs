using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_Books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableBooks",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeleteReasoning",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DeleteStamp",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableBooks",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeleteReasoning",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeleteStamp",
                table: "Books");
        }
    }
}
