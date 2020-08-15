using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AlterPropsLife : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBaptismOhterChurch",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "MinisterBaptism",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lifes");

            migrationBuilder.AddColumn<string>(
                name: "BaptismMinister",
                table: "Lifes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BaptismOtherChurch",
                table: "Lifes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Lifes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaptismMinister",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "BaptismOtherChurch",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Lifes");

            migrationBuilder.AddColumn<bool>(
                name: "IsBaptismOhterChurch",
                table: "Lifes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MinisterBaptism",
                table: "Lifes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lifes",
                type: "text",
                nullable: true);
        }
    }
}
