using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nodes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRemindersSerialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReminderIds",
                schema: "Nodes",
                table: "Nodes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderIds",
                schema: "Nodes",
                table: "Nodes");
        }
    }
}
