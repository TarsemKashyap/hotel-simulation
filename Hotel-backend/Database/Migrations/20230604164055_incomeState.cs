using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class incomeState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeState",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MonthID = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Room1 = table.Column<int>(type: "int", nullable: false),
                    FoodB = table.Column<int>(type: "int", nullable: false),
                    FoodB1 = table.Column<int>(type: "int", nullable: false),
                    FoodB2 = table.Column<int>(type: "int", nullable: false),
                    FoodB3 = table.Column<int>(type: "int", nullable: false),
                    FoodB4 = table.Column<int>(type: "int", nullable: false),
                    FoodB5 = table.Column<int>(type: "int", nullable: false),
                    Other = table.Column<int>(type: "int", nullable: false),
                    Other1 = table.Column<int>(type: "int", nullable: false),
                    Other2 = table.Column<int>(type: "int", nullable: false),
                    Other3 = table.Column<int>(type: "int", nullable: false),
                    Other4 = table.Column<int>(type: "int", nullable: false),
                    Other5 = table.Column<int>(type: "int", nullable: false),
                    Other6 = table.Column<int>(type: "int", nullable: false),
                    Rent = table.Column<int>(type: "int", nullable: false),
                    TotReven = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    Food2B = table.Column<int>(type: "int", nullable: false),
                    Other7 = table.Column<int>(type: "int", nullable: false),
                    TotExpen = table.Column<int>(type: "int", nullable: false),
                    TotDeptIncom = table.Column<int>(type: "int", nullable: false),
                    UndisExpens1 = table.Column<int>(type: "int", nullable: false),
                    UndisExpens2 = table.Column<int>(type: "int", nullable: false),
                    UndisExpens3 = table.Column<int>(type: "int", nullable: false),
                    UndisExpens4 = table.Column<int>(type: "int", nullable: false),
                    UndisExpens5 = table.Column<int>(type: "int", nullable: false),
                    UndisExpens6 = table.Column<int>(type: "int", nullable: false),
                    GrossProfit = table.Column<int>(type: "int", nullable: false),
                    MgtFee = table.Column<int>(type: "int", nullable: false),
                    IncomBfCharg = table.Column<int>(type: "int", nullable: false),
                    Property = table.Column<int>(type: "int", nullable: false),
                    Insurance = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<int>(type: "int", nullable: false),
                    PropDepreciationerty = table.Column<int>(type: "int", nullable: false),
                    TotCharg = table.Column<int>(type: "int", nullable: false),
                    NetIncomBfTAX = table.Column<int>(type: "int", nullable: false),
                    Replace = table.Column<int>(type: "int", nullable: false),
                    AjstNetIncom = table.Column<int>(type: "int", nullable: false),
                    IncomTAX = table.Column<int>(type: "int", nullable: false),
                    NetIncom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeState", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeState_ClassMonth_MonthID",
                        column: x => x.MonthID,
                        principalTable: "ClassMonth",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeState_MonthID",
                table: "IncomeState",
                column: "MonthID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeState");
        }
    }
}
