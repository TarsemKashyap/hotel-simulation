using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class WeightedAttributeRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeightedAttributeRating",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    CustomerRating = table.Column<int>(type: "int", nullable: false),
                    ActualDemand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightedAttributeRating", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeightedAttributeRating_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WeightedAttributeRating_MonthID",
                table: "WeightedAttributeRating",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightedAttributeRating");
        }
    }
}
