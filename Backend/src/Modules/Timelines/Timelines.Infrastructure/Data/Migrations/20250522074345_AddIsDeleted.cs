using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timelines.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Timelines",
                table: "Timelines",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "Timelines",
                table: "Timelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Timelines",
                table: "Timelines",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Timelines",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Timelines",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Timelines",
                table: "Timelines");
        }
    }
}
