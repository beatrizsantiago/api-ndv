using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AlterProsFeedbackSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_IntegradorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_AspNetUsers_IntegradorId",
                table: "Lifes");

            migrationBuilder.DropIndex(
                name: "IX_Lifes_IntegradorId",
                table: "Lifes");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_IntegradorId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IntegradorId",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "IntegradorId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoneDate",
                table: "ProgressStepsLifes",
                type: "timestamp(0)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "IntegratorId",
                table: "Lifes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IntegratorId",
                table: "Feedbacks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_IntegratorId",
                table: "Lifes",
                column: "IntegratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IntegratorId",
                table: "Feedbacks",
                column: "IntegratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_IntegratorId",
                table: "Feedbacks",
                column: "IntegratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_AspNetUsers_IntegratorId",
                table: "Lifes",
                column: "IntegratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_IntegratorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifes_AspNetUsers_IntegratorId",
                table: "Lifes");

            migrationBuilder.DropIndex(
                name: "IX_Lifes_IntegratorId",
                table: "Lifes");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_IntegratorId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DoneDate",
                table: "ProgressStepsLifes");

            migrationBuilder.DropColumn(
                name: "IntegratorId",
                table: "Lifes");

            migrationBuilder.DropColumn(
                name: "IntegratorId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<long>(
                name: "IntegradorId",
                table: "Lifes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IntegradorId",
                table: "Feedbacks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lifes_IntegradorId",
                table: "Lifes",
                column: "IntegradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IntegradorId",
                table: "Feedbacks",
                column: "IntegradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_IntegradorId",
                table: "Feedbacks",
                column: "IntegradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifes_AspNetUsers_IntegradorId",
                table: "Lifes",
                column: "IntegradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
