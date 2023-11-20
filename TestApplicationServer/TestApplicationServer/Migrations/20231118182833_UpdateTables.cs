using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplicationServer.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_AppUserId",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserTests");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserTests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_AppUserId",
                table: "UserTests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_AspNetUsers_AppUserId",
                table: "UserTests");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserTests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_AspNetUsers_AppUserId",
                table: "UserTests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
