using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminders.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColorHex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                schema: "Reminders",
                table: "Reminders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                schema: "Reminders",
                table: "Reminders");
        }
    }
}
