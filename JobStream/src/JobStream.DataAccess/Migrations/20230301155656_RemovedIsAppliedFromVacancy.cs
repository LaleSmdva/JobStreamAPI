using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class RemovedIsAppliedFromVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isApplied",
                table: "Vacancies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isApplied",
                table: "Vacancies",
                type: "bit",
                nullable: true);
        }
    }
}
