using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "BugReports",
                table: "BugReports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "BugReports",
                table: "BugReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "BugReports",
                table: "BugReports",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "BugReports",
                table: "BugReports");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "BugReports",
                table: "BugReports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "BugReports",
                table: "BugReports");
        }
    }
}
