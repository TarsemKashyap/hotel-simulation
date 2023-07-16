using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Migrations
{
    public partial class WeekdayVSsegmentConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<bool>(
            //    name: "isDefault",
            //    table: "StudentClassMapping",
            //    type: "tinyint(1)",
            //    nullable: false,
            //    oldClrType: typeof(bool),
            //    oldType: "tinyint(1)",
            //    oldDefaultValue: false);

            //migrationBuilder.CreateTable(
            //    name: "MarketingVSsegmentConfig",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
            //        ConfigID = table.Column<int>(type: "int", nullable: false),
            //        Segment = table.Column<string>(type: "longtext", nullable: false),
            //        MarketingTechniques = table.Column<string>(type: "longtext", nullable: false),
            //        Percentage = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
            //        LaborPercent = table.Column<double>(type: "double", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MarketingVSsegmentConfig", x => x.ID);
            //    })
            //    .Annotation("MySQL:Charset", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "PriceMarketingAttributeSegmentConfig",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
            //        ConfigID = table.Column<int>(type: "int", nullable: false),
            //        PMA = table.Column<string>(type: "longtext", nullable: false),
            //        Segment = table.Column<string>(type: "longtext", nullable: false),
            //        Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PriceMarketingAttributeSegmentConfig", x => x.ID);
            //    })
            //    .Annotation("MySQL:Charset", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "SegmentConfig",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
            //        ConfigID = table.Column<int>(type: "int", nullable: false),
            //        Segment = table.Column<string>(type: "longtext", nullable: false),
            //        Percentage = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SegmentConfig", x => x.ID);
            //    })
            //    .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeekdayVSsegmentConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ConfigID = table.Column<int>(type: "int", nullable: false),
                    WeekDay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Segment = table.Column<string>(type: "longtext", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceExpectation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekdayVSsegmentConfig", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StudentGroupMapping_ClassGroup_ClassGroupGroupId",
            //    table: "StudentGroupMapping",
            //    column: "ClassGroupGroupId",
            //    principalTable: "ClassGroup",
            //    principalColumn: "GroupId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StudentRoleMapping_Student_StudentId",
            //    table: "StudentRoleMapping",
            //    column: "StudentId",
            //    principalTable: "Student",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StudentRoleMapping_StudentRoles_RoleId",
            //    table: "StudentRoleMapping",
            //    column: "RoleId",
            //    principalTable: "StudentRoles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroupMapping_ClassGroup_ClassGroupGroupId",
                table: "StudentGroupMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRoleMapping_Student_StudentId",
                table: "StudentRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRoleMapping_StudentRoles_RoleId",
                table: "StudentRoleMapping");

            migrationBuilder.DropTable(
                name: "MarketingVSsegmentConfig");

            migrationBuilder.DropTable(
                name: "PriceMarketingAttributeSegmentConfig");

            migrationBuilder.DropTable(
                name: "SegmentConfig");

            migrationBuilder.DropTable(
                name: "WeekdayVSsegmentConfig");

            migrationBuilder.AlterColumn<bool>(
                name: "isDefault",
                table: "StudentClassMapping",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
