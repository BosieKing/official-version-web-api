using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDataSphere.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "T_PersonalCourseSignUp");

            migrationBuilder.DropColumn(
                name: "IsUse",
                table: "T_PersonalCourseSignUp");

            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "T_CourseSignUp");

            migrationBuilder.DropColumn(
                name: "IsUse",
                table: "T_CourseSignUp");

            migrationBuilder.AddColumn<long>(
                name: "CardId",
                table: "T_PersonalCourseSignUp",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CardId",
                table: "T_CourseSignUp",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardId",
                table: "T_PersonalCourseSignUp");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "T_CourseSignUp");

            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "T_PersonalCourseSignUp",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUse",
                table: "T_PersonalCourseSignUp",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "T_CourseSignUp",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUse",
                table: "T_CourseSignUp",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
