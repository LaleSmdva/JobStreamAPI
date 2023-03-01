using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducation_CandidateResume_CandidateResumeId",
                table: "CandidateEducation");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducation_CandidateResumeId",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "CandidateResumeId",
                table: "CandidateEducation");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Vacancies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Vacancies");

            migrationBuilder.AddColumn<int>(
                name: "CandidateResumeId",
                table: "CandidateEducation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducation_CandidateResumeId",
                table: "CandidateEducation",
                column: "CandidateResumeId",
                unique: true,
                filter: "[CandidateResumeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducation_CandidateResume_CandidateResumeId",
                table: "CandidateEducation",
                column: "CandidateResumeId",
                principalTable: "CandidateResume",
                principalColumn: "Id");
        }
    }
}
