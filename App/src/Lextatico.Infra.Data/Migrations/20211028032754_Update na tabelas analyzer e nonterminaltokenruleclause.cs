using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lextatico.Infra.Data.Migrations
{
    public partial class Updatenatabelasanalyzerenonterminaltokenruleclause : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                column: "TerminalTokenId",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause");

            migrationBuilder.AlterColumn<Guid>(
                name: "TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NonTerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NonTerminalTokenRuleClause_TerminalToken_TerminalTokenId",
                table: "NonTerminalTokenRuleClause",
                column: "TerminalTokenId",
                principalTable: "TerminalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
