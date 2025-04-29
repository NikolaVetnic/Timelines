using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reminders.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixReminderProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDateTime",
                schema: "Reminders",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Reminders",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "NotificationTime",
                schema: "Reminders",
                table: "Reminders",
                newName: "NotifyAt");

            migrationBuilder.AddColumn<string>(
                name: "RelatedReminders",
                schema: "Reminders",
                table: "Reminders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedReminders",
                schema: "Reminders",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "NotifyAt",
                schema: "Reminders",
                table: "Reminders",
                newName: "NotificationTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTime",
                schema: "Reminders",
                table: "Reminders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Reminders",
                table: "Reminders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
