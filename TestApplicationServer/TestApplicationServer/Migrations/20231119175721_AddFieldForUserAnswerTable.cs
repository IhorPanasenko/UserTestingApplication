using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplicationServer.Migrations
{
    public partial class AddFieldForUserAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "questionTypeName",
                table: "QuestionTypes",
                newName: "QuestionTypeName");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "UserAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "UserAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionTypeName",
                table: "QuestionTypes",
                newName: "questionTypeName");
        }
    }
}
