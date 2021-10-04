using Lextatico.Domain.Models;
using Lextatico.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lextatico.Infra.Data.Configurations
{
    public class AnalyzerConfiguration : IEntityTypeConfiguration<Analyzer>
    {
        public void Configure(EntityTypeBuilder<Analyzer> builder)
        {
            builder.DefineDefaultFields(nameof(Analyzer));

            builder.Property(model => model.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();
        }
    }
}