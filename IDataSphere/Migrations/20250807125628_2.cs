using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDataSphere.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "T_PersonalCourse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "T_PersonalCourse",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
