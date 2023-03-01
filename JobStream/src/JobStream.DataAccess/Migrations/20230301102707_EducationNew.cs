using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class EducationNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "CandidateEducation");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "CandidateEducation");

            migrationBuilder.AddColumn<string>(
                name: "EducationInfo",
                table: "CandidateEducation",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationInfo",
                table: "CandidateEducation");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "CandidateEducation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "CandidateEducation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "CandidateEducation",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
