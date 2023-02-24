using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class AddGuidToAppUSerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateResume_JobTypes_JobTypeId",
                table: "CandidateResume");

            migrationBuilder.DropIndex(
                name: "IX_CandidateResume_JobTypeId",
                table: "CandidateResume");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "CandidateResume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTypeId",
                table: "CandidateResume",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateResume_JobTypeId",
                table: "CandidateResume",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateResume_JobTypes_JobTypeId",
                table: "CandidateResume",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "Id");
        }
    }
}
