using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class PriceDecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceDecision",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<string>(type: "longtext", nullable: false),
                    Weekday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DistributionChannel = table.Column<string>(type: "longtext", nullable: true),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ActualDemand = table.Column<int>(type: "int", nullable: false),
                    Confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDecision", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceDecision_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDecision_MonthID",
                table: "PriceDecision",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDecision");
        }
    }
}
