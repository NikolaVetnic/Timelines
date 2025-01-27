using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoteConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importance",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                schema: "Notes",
                table: "Notes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string[]>(
                name: "SharedWith",
                schema: "Notes",
                table: "Notes",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            // Update new rows with values
            migrationBuilder.UpdateData(
                table: "Notes",
                schema: "Notes",
                keyColumn: "Id",
                keyValue: "74dad71c - 4ddc - 4d4d - a894 - 3307ddc3fe10",
                columns: new[] { "IsPublic", "SharedWith" },
                values: new object[] { true, new[] { "user1", "user2" } });

            migrationBuilder.UpdateData(
                table: "Notes",
                schema: "Notes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsPublic", "SharedWith" },
                values: new object[] { true, new string[] {"user1"} });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "SharedWith",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "Importance",
                schema: "Notes",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
