using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Domain.Models;
using Lextatico.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lextatico.Infra.Data.Configurations
{
    public class NonTerminalTokenRuleClauseConfiguration : IEntityTypeConfiguration<NonTerminalTokenRuleClause>
    {
        public void Configure(EntityTypeBuilder<NonTerminalTokenRuleClause> builder)
        {
            builder.DefineDefaultFields();

            builder.Property(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.Sequence)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.IsTerminalToken)
                .HasColumnType("BIT")
                .IsRequired();

            builder.Property(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.TerminalTokenId)
                .IsRequired(false);

            builder.Property(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalTokenId)
                .IsRequired(false);

            builder.HasOne(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalToken)
                .WithMany(nonTerminalToken => nonTerminalToken.NonTerminalTokenRuleClauses)
                .HasForeignKey(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalTokenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.TerminalToken)
                .WithMany(terminalToken => terminalToken.NonTerminalTokenRuleClauses)
                .HasForeignKey(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.TerminalTokenId);

            builder.HasOne(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalTokenRule)
                .WithMany(nonTerminalTokenRule => nonTerminalTokenRule.NonTerminalTokenRuleClauses)
                .HasForeignKey(nonTerminalTokenRuleClause => nonTerminalTokenRuleClause.NonTerminalTokenRuleId);
        }
    }
}
