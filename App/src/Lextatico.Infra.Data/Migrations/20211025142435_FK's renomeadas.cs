using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class FKsrenomeadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerNonTerminalToken_Analyzer_IdAnalyzer",
                table: "AnalyzerNonTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerTerminalToken_Analyzer_IdAnalyzer",
                table: "AnalyzerTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerTerminalToken_TerminalToken_IdTerminalToken",
                table: "AnalyzerTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRule_NonTerminalToken_IdNonTerminalToken",
                table: "NonTerminalTokenRule");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalToken_IdNonTerminalToken",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalTokenRule_IdNonTerminalTokenRule",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_IdTerminalToken",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_IdApplicationUser",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "IdApplicationUser",
                table: "RefreshToken",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_IdApplicationUser",
                table: "RefreshToken",
                newName: "IX_RefreshToken_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "IdTerminalToken",
                table: "NonTerminalTokenRuleClause",
                newName: "TerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdNonTerminalTokenRule",
                table: "NonTerminalTokenRuleClause",
                newName: "NonTerminalTokenRuleId");

            migrationBuilder.RenameColumn(
                name: "IdNonTerminalToken",
                table: "NonTerminalTokenRuleClause",
                newName: "NonTerminalTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_IdTerminalToken",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_TerminalTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_IdNonTerminalTokenRule",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_NonTerminalTokenRuleId");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_IdNonTerminalToken",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_NonTerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdNonTerminalToken",
                table: "NonTerminalTokenRule",
                newName: "NonTerminalTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRule_IdNonTerminalToken",
                table: "NonTerminalTokenRule",
                newName: "IX_NonTerminalTokenRule_NonTerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdTerminalToken",
                table: "AnalyzerTerminalToken",
                newName: "TerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdAnalyzer",
                table: "AnalyzerTerminalToken",
                newName: "AnalyzerId");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerTerminalToken_IdTerminalToken",
                table: "AnalyzerTerminalToken",
                newName: "IX_AnalyzerTerminalToken_TerminalTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerTerminalToken_IdAnalyzer_IdTerminalToken",
                table: "AnalyzerTerminalToken",
                newName: "IX_AnalyzerTerminalToken_AnalyzerId_TerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                newName: "NonTerminalTokenId");

            migrationBuilder.RenameColumn(
                name: "IdAnalyzer",
                table: "AnalyzerNonTerminalToken",
                newName: "AnalyzerId");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerNonTerminalToken_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                newName: "IX_AnalyzerNonTerminalToken_NonTerminalTokenId");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerNonTerminalToken_IdAnalyzer_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                newName: "IX_AnalyzerNonTerminalToken_AnalyzerId_NonTerminalTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerNonTerminalToken_Analyzer_AnalyzerId",
                table: "AnalyzerNonTerminalToken",
                column: "AnalyzerId",
                principalTable: "Analyzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                column: "NonTerminalTokenId",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerTerminalToken_Analyzer_AnalyzerId",
                table: "AnalyzerTerminalToken",
                column: "AnalyzerId",
                principalTable: "Analyzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerTerminalToken_TerminalToken_TerminalTokenId",
                table: "AnalyzerTerminalToken",
                column: "TerminalTokenId",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRule_NonTerminalToken_NonTerminalTokenId",
                table: "NonTerminalTokenRule",
                column: "NonTerminalTokenId",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalToken_NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                column: "NonTerminalTokenId",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalTokenRule_NonTerminalTokenRuleId",
                table: "NonTerminalTokenRuleClause",
                column: "NonTerminalTokenRuleId",
                principalTable: "NonTerminalTokenRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                column: "TerminalTokenId",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerNonTerminalToken_Analyzer_AnalyzerId",
                table: "AnalyzerNonTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerTerminalToken_Analyzer_AnalyzerId",
                table: "AnalyzerTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzerTerminalToken_TerminalToken_TerminalTokenId",
                table: "AnalyzerTerminalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRule_NonTerminalToken_NonTerminalTokenId",
                table: "NonTerminalTokenRule");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalToken_NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalTokenRule_NonTerminalTokenRuleId",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "RefreshToken",
                newName: "IdApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_IdApplicationUser");

            migrationBuilder.RenameColumn(
                name: "TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                newName: "IdTerminalToken");

            migrationBuilder.RenameColumn(
                name: "NonTerminalTokenRuleId",
                table: "NonTerminalTokenRuleClause",
                newName: "IdNonTerminalTokenRule");

            migrationBuilder.RenameColumn(
                name: "NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                newName: "IdNonTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_IdTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_NonTerminalTokenRuleId",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_IdNonTerminalTokenRule");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRuleClause_NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                newName: "IX_NonTerminalTokenRuleClause_IdNonTerminalToken");

            migrationBuilder.RenameColumn(
                name: "NonTerminalTokenId",
                table: "NonTerminalTokenRule",
                newName: "IdNonTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_NonTerminalTokenRule_NonTerminalTokenId",
                table: "NonTerminalTokenRule",
                newName: "IX_NonTerminalTokenRule_IdNonTerminalToken");

            migrationBuilder.RenameColumn(
                name: "TerminalTokenId",
                table: "AnalyzerTerminalToken",
                newName: "IdTerminalToken");

            migrationBuilder.RenameColumn(
                name: "AnalyzerId",
                table: "AnalyzerTerminalToken",
                newName: "IdAnalyzer");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerTerminalToken_TerminalTokenId",
                table: "AnalyzerTerminalToken",
                newName: "IX_AnalyzerTerminalToken_IdTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerTerminalToken_AnalyzerId_TerminalTokenId",
                table: "AnalyzerTerminalToken",
                newName: "IX_AnalyzerTerminalToken_IdAnalyzer_IdTerminalToken");

            migrationBuilder.RenameColumn(
                name: "NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                newName: "IdNonTerminalToken");

            migrationBuilder.RenameColumn(
                name: "AnalyzerId",
                table: "AnalyzerNonTerminalToken",
                newName: "IdAnalyzer");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerNonTerminalToken_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                newName: "IX_AnalyzerNonTerminalToken_IdNonTerminalToken");

            migrationBuilder.RenameIndex(
                name: "IX_AnalyzerNonTerminalToken_AnalyzerId_NonTerminalTokenId",
                table: "AnalyzerNonTerminalToken",
                newName: "IX_AnalyzerNonTerminalToken_IdAnalyzer_IdNonTerminalToken");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerNonTerminalToken_Analyzer_IdAnalyzer",
                table: "AnalyzerNonTerminalToken",
                column: "IdAnalyzer",
                principalTable: "Analyzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerNonTerminalToken_NonTerminalToken_IdNonTerminalToken",
                table: "AnalyzerNonTerminalToken",
                column: "IdNonTerminalToken",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerTerminalToken_Analyzer_IdAnalyzer",
                table: "AnalyzerTerminalToken",
                column: "IdAnalyzer",
                principalTable: "Analyzer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzerTerminalToken_TerminalToken_IdTerminalToken",
                table: "AnalyzerTerminalToken",
                column: "IdTerminalToken",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRule_NonTerminalToken_IdNonTerminalToken",
                table: "NonTerminalTokenRule",
                column: "IdNonTerminalToken",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalToken_IdNonTerminalToken",
                table: "NonTerminalTokenRuleClause",
                column: "IdNonTerminalToken",
                principalTable: "NonTerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_NonTerminalTokenRule_IdNonTerminalTokenRule",
                table: "NonTerminalTokenRuleClause",
                column: "IdNonTerminalTokenRule",
                principalTable: "NonTerminalTokenRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_IdTerminalToken",
                table: "NonTerminalTokenRuleClause",
                column: "IdTerminalToken",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_IdApplicationUser",
                table: "RefreshToken",
                column: "IdApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
