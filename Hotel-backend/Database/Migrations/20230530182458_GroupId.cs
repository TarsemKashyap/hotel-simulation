using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class GroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "StudentClassMapping",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 30, 23, 54, 58, 562, DateTimeKind.Local).AddTicks(7606),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 24, 21, 52, 21, 288, DateTimeKind.Local).AddTicks(180));

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassMapping_GroupId",
                table: "StudentClassMapping",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassMapping_ClassGroup_GroupId",
                table: "StudentClassMapping",
                column: "GroupId",
                principalTable: "ClassGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassMapping_ClassGroup_GroupId",
                table: "StudentClassMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassMapping_GroupId",
                table: "StudentClassMapping");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "StudentClassMapping");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 24, 21, 52, 21, 288, DateTimeKind.Local).AddTicks(180),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 30, 23, 54, 58, 562, DateTimeKind.Local).AddTicks(7606));
        }
    }
}
