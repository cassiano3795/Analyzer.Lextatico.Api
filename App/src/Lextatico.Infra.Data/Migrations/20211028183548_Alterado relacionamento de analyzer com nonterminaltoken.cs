using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class Alteradorelacionamentodeanalyzercomnonterminaltoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzerNonTerminalToken");

            migrationBuilder.AddColumn<Guid>(
                name: "AnalyzerId",
                table: "NonTerminalToken",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_NonTerminalToken_AnalyzerId",
                table: "NonTerminalToken",
                column: "AnalyzerId");

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalToken_Analyzer_AnalyzerId",
                table: "NonTerminalToken",
                column: "AnalyzerId",
                principalTable: "Analyzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalToken_Analyzer_AnalyzerId",
                table: "NonTerminalToken");

            migrationBuilder.DropIndex(
                name: "IX_NonTerminalToken_AnalyzerId",
                table: "NonTerminalToken");

            migrationBuilder.DropColumn(
                name: "AnalyzerId",
                table: "NonTerminalToken");

            migrationBuilder.CreateTable(
                name: "AnalyzerNonTerminalToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AnalyzerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    NonTerminalTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerNonTerminalToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyzerNonTerminalToken_Analyzer_AnalyzerId",
                        column: x => x.AnalyzerId,
                        principalTable: "Analyzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_NonTerminalTokenId",
                        column: x => x.NonTerminalTokenId,
                        principalTable: "NonTerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerNonTerminalToken_AnalyzerId_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                columns: new[] { "AnalyzerId", "NonTerminalTokenId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerNonTerminalToken_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                column: "NonTerminalTokenId");
        }
    }
}
