using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 11, 20, 27, 49, 62, DateTimeKind.Local).AddTicks(9517),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 7, 21, 12, 41, 345, DateTimeKind.Local).AddTicks(5346));

            migrationBuilder.InsertData(
                table: "StudentRoleMapping",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "Revenue Manager" });

            migrationBuilder.InsertData(
                table: "StudentRoleMapping",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "Room Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentRoleMapping",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentRoleMapping",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 7, 21, 12, 41, 345, DateTimeKind.Local).AddTicks(5346),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 11, 20, 27, 49, 62, DateTimeKind.Local).AddTicks(9517));
        }
    }
}
