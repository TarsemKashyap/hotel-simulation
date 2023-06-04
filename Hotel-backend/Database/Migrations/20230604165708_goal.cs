using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class goal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    OccupancyM = table.Column<int>(type: "int", nullable: false),
                    OccupancyY = table.Column<int>(type: "int", nullable: false),
                    RoomRevenM = table.Column<int>(type: "int", nullable: false),
                    RoomRevenY = table.Column<int>(type: "int", nullable: false),
                    TotalRevenM = table.Column<int>(type: "int", nullable: false),
                    TotalRevenY = table.Column<int>(type: "int", nullable: false),
                    ShareRoomM = table.Column<int>(type: "int", nullable: false),
                    ShareRoomY = table.Column<int>(type: "int", nullable: false),
                    ShareRevenM = table.Column<int>(type: "int", nullable: false),
                    ShareRevenY = table.Column<int>(type: "int", nullable: false),
                    RevparM = table.Column<int>(type: "int", nullable: false),
                    RevparY = table.Column<int>(type: "int", nullable: false),
                    ADRM = table.Column<int>(type: "int", nullable: false),
                    ADRY = table.Column<int>(type: "int", nullable: false),
                    YieldMgtM = table.Column<int>(type: "int", nullable: false),
                    YieldMgtY = table.Column<int>(type: "int", nullable: false),
                    MgtEfficiencyM = table.Column<int>(type: "int", nullable: false),
                    MgtEfficiencyY = table.Column<int>(type: "int", nullable: false),
                    ProfitMarginM = table.Column<int>(type: "int", nullable: false),
                    ProfitMarginY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Goal_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_MonthID",
                table: "Goal",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goal");
        }
    }
}
