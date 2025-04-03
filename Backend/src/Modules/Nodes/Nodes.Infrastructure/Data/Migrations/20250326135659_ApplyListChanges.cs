using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nodes.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplyListChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Categories",
                schema: "Nodes",
                table: "Nodes",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Tags",
                schema: "Nodes",
                table: "Nodes",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                schema: "Nodes",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "Nodes",
                table: "Nodes");
        }
    }
}
