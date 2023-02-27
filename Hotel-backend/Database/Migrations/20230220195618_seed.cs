using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__MigrationScript",
                columns: table => new
                {
                    ScriptId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    ExecutedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2023, 2, 21, 1, 26, 17, 944, DateTimeKind.Local).AddTicks(4440))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___MigrationScript", x => x.ScriptId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__MigrationScript");
        }
    }
}
