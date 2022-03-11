using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class Updatenastabelasparaoanalisadorsintatico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzerToken");

            migrationBuilder.CreateTable(
                name: "AnalyzerNonTerminalToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    IdAnalyzer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNonTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerNonTerminalToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyzerNonTerminalToken_Analyzer_IdAnalyzer",
                        column: x => x.IdAnalyzer,
                        principalTable: "Analyzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_IdNonTerminalToken",
                        column: x => x.IdNonTerminalToken,
                        principalTable: "NonTerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalyzerTerminalToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    IdAnalyzer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerTerminalToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyzerTerminalToken_Analyzer_IdAnalyzer",
                        column: x => x.IdAnalyzer,
                        principalTable: "Analyzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerTerminalToken_TerminalToken_IdTerminalToken",
                        column: x => x.IdTerminalToken,
                        principalTable: "TerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerNonTerminalToken_IdAnalyzer_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                columns: new[] { "IdAnalyzer", "IdNonTerminalToken" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerNonTerminalToken_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                column: "IdNonTerminalToken");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerTerminalToken_IdAnalyzer_IdTerminalToken",
                table: "AnalyzerTerminalToken",
                columns: new[] { "IdAnalyzer", "IdTerminalToken" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerTerminalToken_IdTerminalToken",
                table: "AnalyzerTerminalToken",
                column: "IdTerminalToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzerNonTerminalToken");

            migrationBuilder.DropTable(
                name: "AnalyzerTerminalToken");

            migrationBuilder.CreateTable(
                name: "AnalyzerToken",
                columns: table => new
                {
                    IdAnalyzer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerToken", x => new { x.IdAnalyzer, x.IdTerminalToken });
                    table.ForeignKey(
                        name: "FK_AnalyzerToken_Analyzer_IdAnalyzer",
                        column: x => x.IdAnalyzer,
                        principalTable: "Analyzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerToken_TerminalToken_IdTerminalToken",
                        column: x => x.IdTerminalToken,
                        principalTable: "TerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerToken_IdTerminalToken",
                table: "AnalyzerToken",
                column: "IdTerminalToken");
        }
    }
}
