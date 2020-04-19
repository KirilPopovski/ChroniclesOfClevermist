using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChroniclesOfClevermist.Data.Migrations
{
    public partial class ChangePollsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Opinion");

            migrationBuilder.CreateTable(
                name: "UserOpinion",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    OpinionId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOpinion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOpinion_Opinion_OpinionId",
                        column: x => x.OpinionId,
                        principalTable: "Opinion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOpinion_IsDeleted",
                table: "UserOpinion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserOpinion_OpinionId",
                table: "UserOpinion",
                column: "OpinionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOpinion");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Surveys",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Opinion",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Option_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_IsDeleted",
                table: "Option",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Option_SurveyId",
                table: "Option",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
