using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class IsDefaultProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDefault",
                table: "StudentClassMapping",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 4, 13, 57, 20, 743, DateTimeKind.Local).AddTicks(5121),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 3, 11, 31, 57, 427, DateTimeKind.Local).AddTicks(2799));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDefault",
                table: "StudentClassMapping");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 3, 11, 31, 57, 427, DateTimeKind.Local).AddTicks(2799),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 4, 13, 57, 20, 743, DateTimeKind.Local).AddTicks(5121));
        }
    }
}
