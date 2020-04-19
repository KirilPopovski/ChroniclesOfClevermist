using Microsoft.EntityFrameworkCore.Migrations;

namespace ChroniclesOfClevermist.Data.Migrations
{
    public partial class ChangePollsDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinion_Surveys_SurveyId",
                table: "Opinion");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOpinion_Opinion_OpinionId",
                table: "UserOpinion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOpinion",
                table: "UserOpinion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Opinion",
                table: "Opinion");

            migrationBuilder.RenameTable(
                name: "UserOpinion",
                newName: "UserOpinions");

            migrationBuilder.RenameTable(
                name: "Opinion",
                newName: "Opinions");

            migrationBuilder.RenameIndex(
                name: "IX_UserOpinion_OpinionId",
                table: "UserOpinions",
                newName: "IX_UserOpinions_OpinionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOpinion_IsDeleted",
                table: "UserOpinions",
                newName: "IX_UserOpinions_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Opinion_SurveyId",
                table: "Opinions",
                newName: "IX_Opinions_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_Opinion_IsDeleted",
                table: "Opinions",
                newName: "IX_Opinions_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOpinions",
                table: "UserOpinions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Opinions",
                table: "Opinions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_Surveys_SurveyId",
                table: "Opinions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOpinions_Opinions_OpinionId",
                table: "UserOpinions",
                column: "OpinionId",
                principalTable: "Opinions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Surveys_SurveyId",
                table: "Opinions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOpinions_Opinions_OpinionId",
                table: "UserOpinions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOpinions",
                table: "UserOpinions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Opinions",
                table: "Opinions");

            migrationBuilder.RenameTable(
                name: "UserOpinions",
                newName: "UserOpinion");

            migrationBuilder.RenameTable(
                name: "Opinions",
                newName: "Opinion");

            migrationBuilder.RenameIndex(
                name: "IX_UserOpinions_OpinionId",
                table: "UserOpinion",
                newName: "IX_UserOpinion_OpinionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserOpinions_IsDeleted",
                table: "UserOpinion",
                newName: "IX_UserOpinion_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Opinions_SurveyId",
                table: "Opinion",
                newName: "IX_Opinion_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_Opinions_IsDeleted",
                table: "Opinion",
                newName: "IX_Opinion_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOpinion",
                table: "UserOpinion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Opinion",
                table: "Opinion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinion_Surveys_SurveyId",
                table: "Opinion",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOpinion_Opinion_OpinionId",
                table: "UserOpinion",
                column: "OpinionId",
                principalTable: "Opinion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
