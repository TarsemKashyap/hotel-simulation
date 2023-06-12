using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class changedatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 7, 21, 12, 41, 345, DateTimeKind.Local).AddTicks(5346),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 7, 20, 52, 7, 32, DateTimeKind.Local).AddTicks(2677));

            migrationBuilder.CreateTable(
                name: "StudentRoleMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRoleMapping", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentRoleMapping");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 7, 20, 52, 7, 32, DateTimeKind.Local).AddTicks(2677),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 7, 21, 12, 41, 345, DateTimeKind.Local).AddTicks(5346));
        }
    }
}
