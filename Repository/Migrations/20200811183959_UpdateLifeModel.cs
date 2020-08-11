using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class UpdateLifeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBaptismOhterChurch",
                table: "Lifes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLost",
                table: "Lifes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MinisterBaptism",
                table: "Lifes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBaptismOhterChurch",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "IsLost",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "MinisterBaptism",
                table: "Lifes");
        }
    }
}
