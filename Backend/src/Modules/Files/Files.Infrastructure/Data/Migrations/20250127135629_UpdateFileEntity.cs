using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Files.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "Files",
                table: "Files",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                schema: "Files",
                table: "Files",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                schema: "Files",
                table: "Files",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Files",
                schema: "Files",
                keyColumn: "Id",
                keyValue: new Guid("16d56e5f-dcea-4b1f-82e3-4c0fdb142773"),
                columns: new[] { "IsPublic", "Type" },
                values: new object[] { true, "Docx" });

            migrationBuilder.UpdateData(
                table: "Files",
                schema: "Files",
                keyColumn: "Id",
                keyValue: new Guid("d79293f2-4910-44e2-bfbb-690a6f24f703"),
                columns: new[] { "IsPublic", "Type" },
                values: new object[] { true, "Pdf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                schema: "Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                schema: "Files",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "Files",
                table: "Files",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
