using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Process : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "request");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "request");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "request");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "request");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_sequence_requiredrole",
                table: "workflow_sequence",
                column: "requiredrole",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookRequests_ProcessId",
                table: "BookRequests",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRequests_process_ProcessId",
                table: "BookRequests",
                column: "ProcessId",
                principalTable: "process",
                principalColumn: "id_process",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workflow_sequence_AspNetRoles_requiredrole",
                table: "workflow_sequence",
                column: "requiredrole",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRequests_process_ProcessId",
                table: "BookRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_workflow_sequence_AspNetRoles_requiredrole",
                table: "workflow_sequence");

            migrationBuilder.DropIndex(
                name: "IX_workflow_sequence_requiredrole",
                table: "workflow_sequence");

            migrationBuilder.DropIndex(
                name: "IX_BookRequests_ProcessId",
                table: "BookRequests");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "request",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "request",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "request",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "request",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
