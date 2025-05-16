using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timelines.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdForPhase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhaseIds",
                schema: "Timelines",
                table: "Timelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                schema: "Timelines",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TimelineId",
                schema: "Timelines",
                table: "Phases",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Phases_TimelineId",
                schema: "Timelines",
                table: "Phases",
                column: "TimelineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phases_TimelineId",
                schema: "Timelines",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "PhaseIds",
                schema: "Timelines",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Timelines",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "TimelineId",
                schema: "Timelines",
                table: "Phases");
        }
    }
}
