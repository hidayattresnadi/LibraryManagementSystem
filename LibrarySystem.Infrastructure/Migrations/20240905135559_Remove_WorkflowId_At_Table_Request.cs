using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_WorkflowId_At_Table_Request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_request_workflow_WorkflowId",
                table: "request");

            migrationBuilder.DropIndex(
                name: "IX_request_WorkflowId",
                table: "request");

            migrationBuilder.DropColumn(
                name: "WorkflowId",
                table: "request");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkflowId",
                table: "request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_request_WorkflowId",
                table: "request",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_request_workflow_WorkflowId",
                table: "request",
                column: "WorkflowId",
                principalTable: "workflow",
                principalColumn: "id_workflow",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
