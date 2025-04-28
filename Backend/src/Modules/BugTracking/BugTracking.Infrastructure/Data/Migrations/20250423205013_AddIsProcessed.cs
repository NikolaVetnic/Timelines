using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsProcessed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
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
                name: "IsProcessed",
                schema: "BugReports",
                table: "BugReports");
        }
    }
}
