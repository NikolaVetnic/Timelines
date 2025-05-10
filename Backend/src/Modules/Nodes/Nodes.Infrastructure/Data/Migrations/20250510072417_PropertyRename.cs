using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nodes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PropertyRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phase",
                schema: "Nodes",
                table: "Nodes");

            migrationBuilder.AddColumn<string>(
                name: "NodeIds",
                schema: "Nodes",
                table: "Phases",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhaseId",
                schema: "Nodes",
                table: "Nodes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_PhaseId",
                schema: "Nodes",
                table: "Nodes",
                column: "PhaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nodes_PhaseId",
                schema: "Nodes",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "NodeIds",
                schema: "Nodes",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                schema: "Nodes",
                table: "Nodes");

            migrationBuilder.AddColumn<string>(
                name: "Phase",
                schema: "Nodes",
                table: "Nodes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
