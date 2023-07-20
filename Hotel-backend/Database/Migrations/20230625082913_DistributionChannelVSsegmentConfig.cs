using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class DistributionChannelVSsegmentConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributionChannelVSsegmentConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ConfigID = table.Column<int>(type: "int", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: false),
                    DistributionChannel = table.Column<string>(type: "longtext", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CostPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionChannelVSsegmentConfig", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributionChannelVSsegmentConfig");
        }
    }
}
