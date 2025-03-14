using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nodes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimelines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TimelineId",
                schema: "Nodes",
                table: "Nodes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_TimelineId",
                schema: "Nodes",
                table: "Nodes",
                column: "TimelineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nodes_TimelineId",
                schema: "Nodes",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "TimelineId",
                schema: "Nodes",
                table: "Nodes");
        }
    }
}
