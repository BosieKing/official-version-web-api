using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDataSphere.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_MyCard_T_User_TeacherId",
                table: "T_MyCard");

            migrationBuilder.DropIndex(
                name: "IX_T_MyCard_TeacherId",
                table: "T_MyCard");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "T_MyCard");

            migrationBuilder.CreateIndex(
                name: "IX_T_MyCard_UserId",
                table: "T_MyCard",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_MyCard_T_User_UserId",
                table: "T_MyCard",
                column: "UserId",
                principalTable: "T_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_MyCard_T_User_UserId",
                table: "T_MyCard");

            migrationBuilder.DropIndex(
                name: "IX_T_MyCard_UserId",
                table: "T_MyCard");

            migrationBuilder.AddColumn<long>(
                name: "TeacherId",
                table: "T_MyCard",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_T_MyCard_TeacherId",
                table: "T_MyCard",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_MyCard_T_User_TeacherId",
                table: "T_MyCard",
                column: "TeacherId",
                principalTable: "T_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
