using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibrarySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adding_PK_WorkflowSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "next_step_rule_id_currentstep_fkey",
                table: "next_step_rule");

            migrationBuilder.DropForeignKey(
                name: "next_step_rule_id_nextstep_fkey",
                table: "next_step_rule");

            migrationBuilder.DropForeignKey(
                name: "FK_process_workflow_sequence_id_current_step",
                table: "process");

            migrationBuilder.DropForeignKey(
                name: "FK_workflow_action_workflow_sequence_id_step",
                table: "workflow_action");

            migrationBuilder.DropPrimaryKey(
                name: "workflow_sequence_pkey",
                table: "workflow_sequence");

            migrationBuilder.AlterColumn<int>(
                name: "step_id",
                table: "workflow_sequence",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "workflow_sequence_pkey",
                table: "workflow_sequence",
                column: "step_id");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_sequence_id_workflow",
                table: "workflow_sequence",
                column: "id_workflow");

            migrationBuilder.AddForeignKey(
                name: "next_step_rule_id_currentstep_fkey",
                table: "next_step_rule",
                column: "id_currentstep",
                principalTable: "workflow_sequence",
                principalColumn: "step_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "next_step_rule_id_nextstep_fkey",
                table: "next_step_rule",
                column: "id_nextstep",
                principalTable: "workflow_sequence",
                principalColumn: "step_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_process_workflow_sequence_id_current_step",
                table: "process",
                column: "id_current_step",
                principalTable: "workflow_sequence",
                principalColumn: "step_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workflow_action_workflow_sequence_id_step",
                table: "workflow_action",
                column: "id_step",
                principalTable: "workflow_sequence",
                principalColumn: "step_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "next_step_rule_id_currentstep_fkey",
                table: "next_step_rule");

            migrationBuilder.DropForeignKey(
                name: "next_step_rule_id_nextstep_fkey",
                table: "next_step_rule");

            migrationBuilder.DropForeignKey(
                name: "FK_process_workflow_sequence_id_current_step",
                table: "process");

            migrationBuilder.DropForeignKey(
                name: "FK_workflow_action_workflow_sequence_id_step",
                table: "workflow_action");

            migrationBuilder.DropPrimaryKey(
                name: "workflow_sequence_pkey",
                table: "workflow_sequence");

            migrationBuilder.DropIndex(
                name: "IX_workflow_sequence_id_workflow",
                table: "workflow_sequence");

            migrationBuilder.AlterColumn<int>(
                name: "step_id",
                table: "workflow_sequence",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "workflow_sequence_pkey",
                table: "workflow_sequence",
                column: "id_workflow");

            migrationBuilder.AddForeignKey(
                name: "next_step_rule_id_currentstep_fkey",
                table: "next_step_rule",
                column: "id_currentstep",
                principalTable: "workflow_sequence",
                principalColumn: "id_workflow",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "next_step_rule_id_nextstep_fkey",
                table: "next_step_rule",
                column: "id_nextstep",
                principalTable: "workflow_sequence",
                principalColumn: "id_workflow",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_process_workflow_sequence_id_current_step",
                table: "process",
                column: "id_current_step",
                principalTable: "workflow_sequence",
                principalColumn: "id_workflow",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workflow_action_workflow_sequence_id_step",
                table: "workflow_action",
                column: "id_step",
                principalTable: "workflow_sequence",
                principalColumn: "id_workflow",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
