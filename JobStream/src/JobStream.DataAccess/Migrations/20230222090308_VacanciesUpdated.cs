using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class VacanciesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_JobSchedule_JobScheduleId1",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_JobScheduleId1",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "JobScheduleId1",
                table: "Vacancies");

            migrationBuilder.AlterColumn<int>(
                name: "JobScheduleId",
                table: "Vacancies",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_JobScheduleId",
                table: "Vacancies",
                column: "JobScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_JobSchedule_JobScheduleId",
                table: "Vacancies",
                column: "JobScheduleId",
                principalTable: "JobSchedule",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_JobSchedule_JobScheduleId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_JobScheduleId",
                table: "Vacancies");

            migrationBuilder.AlterColumn<string>(
                name: "JobScheduleId",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobScheduleId1",
                table: "Vacancies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_JobScheduleId1",
                table: "Vacancies",
                column: "JobScheduleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_JobSchedule_JobScheduleId1",
                table: "Vacancies",
                column: "JobScheduleId1",
                principalTable: "JobSchedule",
                principalColumn: "Id");
        }
    }
}
