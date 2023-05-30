using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class studentrolemappingstudentroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentRoleMapping",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentRoleMapping",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "StudentRoleMapping");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "StudentClassMapping");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "StudentRoleMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentRoleMapping",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 24, 21, 52, 21, 288, DateTimeKind.Local).AddTicks(180),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 20, 1, 14, 167, DateTimeKind.Local).AddTicks(8823));

            migrationBuilder.CreateTable(
                name: "StudentRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StudentRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "Revenue Manager" });

            migrationBuilder.InsertData(
                table: "StudentRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "Room Manager" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentRoleMapping_RoleId",
                table: "StudentRoleMapping",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRoleMapping_StudentId",
                table: "StudentRoleMapping",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRoleMapping_Student_StudentId",
                table: "StudentRoleMapping",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRoleMapping_StudentRoles_RoleId",
                table: "StudentRoleMapping",
                column: "RoleId",
                principalTable: "StudentRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRoleMapping_Student_StudentId",
                table: "StudentRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRoleMapping_StudentRoles_RoleId",
                table: "StudentRoleMapping");

            migrationBuilder.DropTable(
                name: "StudentRoles");

            migrationBuilder.DropIndex(
                name: "IX_StudentRoleMapping_RoleId",
                table: "StudentRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_StudentRoleMapping_StudentId",
                table: "StudentRoleMapping");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "StudentRoleMapping");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentRoleMapping");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "StudentRoleMapping",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "StudentClassMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 20, 1, 14, 167, DateTimeKind.Local).AddTicks(8823),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 24, 21, 52, 21, 288, DateTimeKind.Local).AddTicks(180));

            migrationBuilder.InsertData(
                table: "StudentRoleMapping",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "Revenue Manager" });

            migrationBuilder.InsertData(
                table: "StudentRoleMapping",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "Room Manager" });
        }
    }
}
