using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class LinkentreAnalyzereApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Analyzer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Analyzer_ApplicationUserId",
                table: "Analyzer",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzer_AspNetUsers_ApplicationUserId",
                table: "Analyzer",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzer_AspNetUsers_ApplicationUserId",
                table: "Analyzer");

            migrationBuilder.DropIndex(
                name: "IX_Analyzer_ApplicationUserId",
                table: "Analyzer");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Analyzer");
        }
    }
}
