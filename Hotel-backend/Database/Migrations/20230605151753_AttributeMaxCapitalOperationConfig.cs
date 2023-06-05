using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class AttributeMaxCapitalOperationConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeMaxCapitalOperationConfig",
                columns: table => new
                {
                    ConfigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(type: "longtext", nullable: true),
                    MaxNewCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewCapitalPortion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxOperation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationPortion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaborPortion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreCapitalPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreLaborPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciationYearly = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeMaxCapitalOperationConfig", x => x.ConfigID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeMaxCapitalOperationConfig");
        }
    }
}
