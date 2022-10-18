using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredDate",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "Comments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
