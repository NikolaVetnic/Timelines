using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminders.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                schema: "Reminders",
                table: "Reminders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_NodeId",
                schema: "Reminders",
                table: "Reminders",
                column: "NodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reminders_NodeId",
                schema: "Reminders",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "NodeId",
                schema: "Reminders",
                table: "Reminders");
        }
    }
}
