using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Files.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSizeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                schema: "Files",
                table: "Files",
                type: "text",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Size",
                schema: "Files",
                table: "Files",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
