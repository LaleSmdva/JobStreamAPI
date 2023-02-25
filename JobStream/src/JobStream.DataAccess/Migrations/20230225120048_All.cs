using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class All : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "CandidateResume",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateResume_AppUserId",
                table: "CandidateResume",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateResume_AspNetUsers_AppUserId",
                table: "CandidateResume",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateResume_AspNetUsers_AppUserId",
                table: "CandidateResume");

            migrationBuilder.DropIndex(
                name: "IX_CandidateResume_AppUserId",
                table: "CandidateResume");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CandidateResume");
        }
    }
}
