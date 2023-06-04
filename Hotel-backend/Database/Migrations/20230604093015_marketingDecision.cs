using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class marketingDecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "quantity",
            //    table: "StudentSignupTemp",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "quantityleft",
            //    table: "StudentSignupTemp",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 5, 21, 24, 50, 981, DateTimeKind.Local).AddTicks(99));

            migrationBuilder.CreateTable(
                name: "MarketingDecision",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    MarketingTechniques = table.Column<string>(type: "longtext", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    Spending = table.Column<int>(type: "int", nullable: false),
                    LaborSpending = table.Column<int>(type: "int", nullable: false),
                    ActualDemand = table.Column<int>(type: "int", nullable: false),
                    Confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingDecision", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MarketingDecision_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingDecision_MonthID",
                table: "MarketingDecision",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketingDecision");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "StudentSignupTemp");

            migrationBuilder.DropColumn(
                name: "quantityleft",
                table: "StudentSignupTemp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExecutedOn",
                table: "__MigrationScript",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 21, 24, 50, 981, DateTimeKind.Local).AddTicks(99),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
