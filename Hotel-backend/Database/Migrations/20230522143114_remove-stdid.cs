using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class removestdid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroupMapping_Student_StudentId",
                table: "StudentGroupMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroupMapping_StudentRoleMapping_StudentRoleMappingId",
                table: "StudentGroupMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroupMapping_StudentId",
                table: "StudentGroupMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentGroupMapping_StudentRoleMappingId",
                table: "StudentGroupMapping");

            migrationBuilder.DropColumn(
                name: "StudentRoleMappingId",
                table: "StudentGroupMapping");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentGroupMapping",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 20, 1, 14, 167, DateTimeKind.Local).AddTicks(8823),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 21, 15, 54, 56, 365, DateTimeKind.Local).AddTicks(7102));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentGroupMapping",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentRoleMappingId",
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
                oldDefaultValue: new DateTime(2023, 5, 22, 20, 1, 14, 167, DateTimeKind.Local).AddTicks(8823));

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_StudentId",
                table: "StudentGroupMapping",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_StudentRoleMappingId",
                table: "StudentGroupMapping",
                column: "StudentRoleMappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroupMapping_Student_StudentId",
                table: "StudentGroupMapping",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroupMapping_StudentRoleMapping_StudentRoleMappingId",
                table: "StudentGroupMapping",
                column: "StudentRoleMappingId",
                principalTable: "StudentRoleMapping",
                principalColumn: "Id");
        }
    }
}
