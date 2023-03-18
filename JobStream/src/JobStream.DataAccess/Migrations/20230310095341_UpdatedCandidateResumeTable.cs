using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class UpdatedCandidateResumeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "EducationInfo",
                table: "CandidateEducation");

            migrationBuilder.AddColumn<string>(
                name: "CandidateEducation",
                table: "CandidateResume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "CandidateEducation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "CandidateEducation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "CandidateEducation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducation_CandidateResumeId",
                table: "CandidateEducation",
                column: "CandidateResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducation_CandidateResume_CandidateResumeId",
                table: "CandidateEducation",
                column: "CandidateResumeId",
                principalTable: "CandidateResume",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducation_CandidateResume_CandidateResumeId",
                table: "CandidateEducation");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducation_CandidateResumeId",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "CandidateEducation",
                table: "CandidateResume");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "CandidateEducation");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Vacancies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationInfo",
                table: "CandidateEducation",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
