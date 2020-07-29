using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Repository.Migrations
{
    public partial class CreatedBaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Net",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhotoProfile",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lifes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    Garbage = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    IntegradorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifes_AspNetUsers_IntegradorId",
                        column: x => x.IntegradorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    Garbage = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IntegradorId = table.Column<long>(nullable: false),
                    LifeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_IntegradorId",
                        column: x => x.IntegradorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Lifes_LifeId",
                        column: x => x.LifeId,
                        principalTable: "Lifes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressStepsLifes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp(0)", nullable: false),
                    Garbage = table.Column<bool>(nullable: false),
                    LifeId = table.Column<string>(nullable: true),
                    Step = table.Column<int>(nullable: false),
                    LifeId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressStepsLifes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressStepsLifes_Lifes_LifeId1",
                        column: x => x.LifeId1,
                        principalTable: "Lifes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IntegradorId",
                table: "Feedbacks",
                column: "IntegradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_LifeId",
                table: "Feedbacks",
                column: "LifeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_IntegradorId",
                table: "Lifes",
                column: "IntegradorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressStepsLifes_LifeId1",
                table: "ProgressStepsLifes",
                column: "LifeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "ProgressStepsLifes");

            migrationBuilder.DropTable(
                name: "Lifes");

            migrationBuilder.DropColumn(
                name: "Net",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoProfile",
                table: "AspNetUsers");
        }
    }
}
