using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class studentgroupmappingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 20, 12, 38, 0, 914, DateTimeKind.Local).AddTicks(857),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 11, 20, 27, 49, 62, DateTimeKind.Local).AddTicks(9517));

            migrationBuilder.CreateTable(
                name: "StudentGroupMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<string>(type: "varchar(255)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StudentRoleMappingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroupMapping_ClassGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ClassGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroupMapping_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentGroupMapping_StudentRoleMapping_StudentRoleMappingId",
                        column: x => x.StudentRoleMappingId,
                        principalTable: "StudentRoleMapping",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_GroupId",
                table: "StudentGroupMapping",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_StudentId",
                table: "StudentGroupMapping",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMapping_StudentRoleMappingId",
                table: "StudentGroupMapping",
                column: "StudentRoleMappingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGroupMapping");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 11, 20, 27, 49, 62, DateTimeKind.Local).AddTicks(9517),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 20, 12, 38, 0, 914, DateTimeKind.Local).AddTicks(857));
        }
    }
}
