using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class addsignup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 29, 19, 45, 43, 951, DateTimeKind.Local).AddTicks(168),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 26, 0, 25, 22, 77, DateTimeKind.Local).AddTicks(5706));

            migrationBuilder.CreateTable(
                name: "StudentSignupTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Institute = table.Column<string>(type: "longtext", nullable: false),
                    ClassCode = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Reference = table.Column<Guid>(type: "char(36)", nullable: false),
                    TransactionId = table.Column<string>(type: "longtext", nullable: true),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PaymentFailureReason = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSignupTemp", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSignupTemp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 26, 0, 25, 22, 77, DateTimeKind.Local).AddTicks(5706),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 29, 19, 45, 43, 951, DateTimeKind.Local).AddTicks(168));
        }
    }
}
