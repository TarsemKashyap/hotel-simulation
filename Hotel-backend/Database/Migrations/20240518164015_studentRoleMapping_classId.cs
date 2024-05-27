using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class studentRoleMapping_classId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "ClassId",
            //    table: "StudentRoleMapping",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_StudentRoleMapping_ClassId",
            //    table: "StudentRoleMapping",
            //    column: "ClassId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StudentRoleMapping_Class_ClassId",
            //    table: "StudentRoleMapping",
            //    column: "ClassId",
            //    principalTable: "Class",
            //    principalColumn: "ClassId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_StudentRoleMapping_Class_ClassId",
            //    table: "StudentRoleMapping");

            //migrationBuilder.DropIndex(
            //    name: "IX_StudentRoleMapping_ClassId",
            //    table: "StudentRoleMapping");

            //migrationBuilder.DropColumn(
            //    name: "ClassId",
            //    table: "StudentRoleMapping");
        }
    }
}
