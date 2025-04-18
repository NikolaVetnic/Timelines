using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoteDataModel : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                schema: "Notes",
                table: "Notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelatedNotes",
                schema: "Notes",
                table: "Notes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SharedWith",
                schema: "Notes",
                table: "Notes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Owner",
                schema: "Notes",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "RelatedNotes",
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
