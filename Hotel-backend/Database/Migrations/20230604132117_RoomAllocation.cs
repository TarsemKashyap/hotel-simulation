using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class RoomAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomAllocation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Weekday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    RoomsAllocated = table.Column<int>(type: "int", nullable: false),
                    ActualDemand = table.Column<int>(type: "int", nullable: false),
                    RoomsSold = table.Column<int>(type: "int", nullable: false),
                    Confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Revenue = table.Column<int>(type: "int", nullable: false),
                    QuarterForecast = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAllocation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomAllocation_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAllocation_MonthID",
                table: "RoomAllocation",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomAllocation");
        }
    }
}
