using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class changetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroupMapping_ClassGroup_GroupId",
                table: "StudentGroupMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroupMapping_GroupId",
                table: "StudentGroupMapping");

            migrationBuilder.AddColumn<int>(
                name: "ClassGroupGroupId",
                table: "StudentGroupMapping",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 21, 15, 54, 56, 365, DateTimeKind.Local).AddTicks(7102),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 20, 12, 38, 0, 914, DateTimeKind.Local).AddTicks(857));

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_ClassGroupGroupId",
                table: "StudentGroupMapping",
                column: "ClassGroupGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroupMapping_ClassGroup_ClassGroupGroupId",
                table: "StudentGroupMapping",
                column: "ClassGroupGroupId",
                principalTable: "ClassGroup",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroupMapping_ClassGroup_ClassGroupGroupId",
                table: "StudentGroupMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroupMapping_ClassGroupGroupId",
                table: "StudentGroupMapping");

            migrationBuilder.DropColumn(
                name: "ClassGroupGroupId",
                table: "StudentGroupMapping");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 20, 12, 38, 0, 914, DateTimeKind.Local).AddTicks(857),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 21, 15, 54, 56, 365, DateTimeKind.Local).AddTicks(7102));

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_GroupId",
                table: "StudentGroupMapping",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroupMapping_ClassGroup_GroupId",
                table: "StudentGroupMapping",
                column: "GroupId",
                principalTable: "ClassGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
