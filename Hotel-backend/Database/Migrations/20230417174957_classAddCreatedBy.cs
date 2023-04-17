using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class classAddCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Class",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 17, 23, 19, 57, 302, DateTimeKind.Local).AddTicks(6500),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 3, 30, 23, 17, 49, 82, DateTimeKind.Local).AddTicks(4056));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Class");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 30, 23, 17, 49, 82, DateTimeKind.Local).AddTicks(4056),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 17, 23, 19, 57, 302, DateTimeKind.Local).AddTicks(6500));
        }
    }
}
