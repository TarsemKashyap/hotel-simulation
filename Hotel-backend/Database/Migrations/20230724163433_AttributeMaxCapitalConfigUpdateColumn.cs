using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class AttributeMaxCapitalConfigUpdateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PreLaborPercent",
                table: "AttributeMaxCapitalOperationConfig",
                type: "decimal(8,8)",
                precision: 8,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreCapitalPercent",
                table: "AttributeMaxCapitalOperationConfig",
                type: "decimal(5,5)",
                precision: 5,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PreLaborPercent",
                table: "AttributeMaxCapitalOperationConfig",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,8)",
                oldPrecision: 8,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "PreCapitalPercent",
                table: "AttributeMaxCapitalOperationConfig",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)",
                oldPrecision: 5,
                oldScale: 5);
        }
    }
}
