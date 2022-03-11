using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class Tabelasiniciaisdoanalisadorsintatico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerToken_TerminalToken_IdToken",
                table: "AnalyzerToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzerToken",
                table: "AnalyzerToken");

            migrationBuilder.DropIndex(
                name: "IX_AnalyzerToken_IdAnalyzer",
                table: "AnalyzerToken");

            migrationBuilder.RenameColumn(
                name: "IdToken",
                table: "AnalyzerToken",
                newName: "IdTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerToken_IdToken",
                table: "AnalyzerToken",
                newName: "IX_AnalyzerToken_IdTerminalToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnalyzerToken",
                table: "AnalyzerToken",
                columns: new[] { "IdAnalyzer", "IdTerminalToken" });

            migrationBuilder.CreateTable(
                name: "NonTerminalToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Sequence = table.Column<int>(type: "INT", nullable: false),
                    IsStart = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonTerminalToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonTerminalTokenRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Sequence = table.Column<int>(type: "INT", nullable: false),
                    IdNonTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonTerminalTokenRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonTerminalTokenRule_NonTerminalToken_IdNonTerminalToken",
                        column: x => x.IdNonTerminalToken,
                        principalTable: "NonTerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NonTerminalTokenRuleClause",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Sequence = table.Column<int>(type: "INT", nullable: false),
                    IsTerminalToken = table.Column<bool>(type: "BIT", nullable: false),
                    IdTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNonTerminalToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNonTerminalTokenRule = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonTerminalTokenRuleClause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonTerminalTokenRuleClause_NonTerminalToken_IdNonTerminalToken",
                        column: x => x.IdNonTerminalToken,
                        principalTable: "NonTerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NonTerminalTokenRuleClause_NonTerminalTokenRule_IdNonTerminalTokenRule",
                        column: x => x.IdNonTerminalTokenRule,
                        principalTable: "NonTerminalTokenRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NonTerminalTokenRuleClause_TerminalToken_IdTerminalToken",
                        column: x => x.IdTerminalToken,
                        principalTable: "TerminalToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonTerminalTokenRule_IdNonTerminalToken",
                table: "NonTerminalTokenRule",
                column: "IdNonTerminalToken");

            migrationBuilder.CreateIndex(
                name: "IX_NonTerminalTokenRuleClause_IdNonTerminalToken",
                table: "NonTerminalTokenRuleClause",
                column: "IdNonTerminalToken");

            migrationBuilder.CreateIndex(
                name: "IX_NonTerminalTokenRuleClause_IdNonTerminalTokenRule",
                table: "NonTerminalTokenRuleClause",
                column: "IdNonTerminalTokenRule");

            migrationBuilder.CreateIndex(
                name: "IX_NonTerminalTokenRuleClause_IdTerminalToken",
                table: "NonTerminalTokenRuleClause",
                column: "IdTerminalToken");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerToken_TerminalToken_IdTerminalToken",
                table: "AnalyzerToken",
                column: "IdTerminalToken",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerToken_TerminalToken_IdTerminalToken",
                table: "AnalyzerToken");

            migrationBuilder.DropTable(
                name: "NonTerminalTokenRuleClause");

            migrationBuilder.DropTable(
                name: "NonTerminalTokenRule");

            migrationBuilder.DropTable(
                name: "NonTerminalToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnalyzerToken",
                table: "AnalyzerToken");

            migrationBuilder.RenameColumn(
                name: "IdTerminalToken",
                table: "AnalyzerToken",
                newName: "IdToken");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerToken_IdTerminalToken",
                table: "AnalyzerToken",
                newName: "IX_AnalyzerToken_IdToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnalyzerToken",
                table: "AnalyzerToken",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerToken_IdAnalyzer",
                table: "AnalyzerToken",
                column: "IdAnalyzer");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerToken_TerminalToken_IdToken",
                table: "AnalyzerToken",
                column: "IdToken",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
