using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Repository.Migrations
{
    public partial class RemoveEntityVisitant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitants");

            migrationBuilder.AddColumn<string>(
                name: "Companion",
                table: "Lifes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FrequentOtherChurch",
                table: "Lifes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Companion",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "FrequentOtherChurch",
                table: "Lifes");

            migrationBuilder.CreateTable(
                name: "Visitants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Companion = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    FrequentOtherChurch = table.Column<bool>(type: "boolean", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Garbage = table.Column<bool>(type: "boolean", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitants", x => x.Id);
                });
        }
    }
}
