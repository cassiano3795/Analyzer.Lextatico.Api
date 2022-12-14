using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analyzer.Lextatico.Domain.Models
{
    public abstract class Base
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;

        public void SetUpdateddAt(DateTime updatedAt) => UpdatedAt = updatedAt;
    }
}
