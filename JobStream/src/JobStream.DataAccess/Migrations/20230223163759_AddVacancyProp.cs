using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class AddVacancyProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Vacancies",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Vacancies");
        }
    }
}
