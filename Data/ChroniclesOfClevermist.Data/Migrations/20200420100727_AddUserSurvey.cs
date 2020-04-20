using Microsoft.EntityFrameworkCore.Migrations;

namespace ChroniclesOfClevermist.Data.Migrations
{
    public partial class AddUserSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersSurveys",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SurveyId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSurveys", x => new { x.UserId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_UsersSurveys_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersSurveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersSurveys_SurveyId",
                table: "UsersSurveys",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersSurveys");
        }
    }
}
