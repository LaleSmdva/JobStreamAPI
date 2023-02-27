using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.DataAccess.Migrations
{
    public partial class AddIsAcceptedColumnToApplicaations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Applications",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Applications");
        }
    }
}
