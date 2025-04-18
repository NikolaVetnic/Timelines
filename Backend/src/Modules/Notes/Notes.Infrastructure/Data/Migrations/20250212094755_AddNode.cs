using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                schema: "Notes",
                table: "Notes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NodeId",
                schema: "Notes",
                table: "Notes",
                column: "NodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_NodeId",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NodeId",
                schema: "Notes",
                table: "Notes");
        }
    }
}
