using Lextatico.Domain.Models;
using Lextatico.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lextatico.Infra.Data.Configurations
{
    public class AnalyzerTerminalTokenConfiguration : IEntityTypeConfiguration<AnalyzerTerminalToken>
    {
        public void Configure(EntityTypeBuilder<AnalyzerTerminalToken> builder)
        {
            builder.DefineDefaultFields();

            builder.Property(analyzerToken => analyzerToken.AnalyzerId)
                .IsRequired();

            builder.Property(analyzerToken => analyzerToken.TerminalTokenId)
                .IsRequired();

            builder.HasIndex(analyzerToken => new { analyzerToken.AnalyzerId, analyzerToken.TerminalTokenId });

            builder.HasOne(analyzerToken => analyzerToken.Analyzer)
                .WithMany(analyzer => analyzer.AnalyzerTerminalTokens)
                .HasForeignKey(analyzerToken => analyzerToken.AnalyzerId);

            builder.HasOne(analyzerToken => analyzerToken.TerminalToken)
                .WithMany(terminalToken => terminalToken.AnalyzerTokens)
                .HasForeignKey(analyzerToken => analyzerToken.TerminalTokenId);
        }
    }
}
