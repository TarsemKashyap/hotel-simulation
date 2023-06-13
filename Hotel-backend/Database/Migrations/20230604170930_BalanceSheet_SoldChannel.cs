using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class BalanceSheet_SoldChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    AcctReceivable = table.Column<int>(type: "int", nullable: false),
                    Inventories = table.Column<int>(type: "int", nullable: false),
                    TotCurrentAsset = table.Column<int>(type: "int", nullable: false),
                    NetPrptyEquip = table.Column<int>(type: "int", nullable: false),
                    TotAsset = table.Column<int>(type: "int", nullable: false),
                    TotCurrentLiab = table.Column<int>(type: "int", nullable: false),
                    LongDebt = table.Column<int>(type: "int", nullable: false),
                    LongDebtPay = table.Column<int>(type: "int", nullable: false),
                    ShortDebt = table.Column<int>(type: "int", nullable: false),
                    ShortDebtPay = table.Column<int>(type: "int", nullable: false),
                    TotLiab = table.Column<int>(type: "int", nullable: false),
                    RetainedEarn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BalanceSheet_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SoldRoomByChannel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    Channel = table.Column<string>(type: "longtext", nullable: true),
                    Weekday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Revenue = table.Column<int>(type: "int", nullable: false),
                    SoldRoom = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldRoomByChannel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SoldRoomByChannel_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_MonthID",
                table: "BalanceSheet",
                column: "MonthID");

            migrationBuilder.CreateIndex(
                name: "IX_SoldRoomByChannel_MonthID",
                table: "SoldRoomByChannel",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheet");

            migrationBuilder.DropTable(
                name: "SoldRoomByChannel");
        }
    }
}
