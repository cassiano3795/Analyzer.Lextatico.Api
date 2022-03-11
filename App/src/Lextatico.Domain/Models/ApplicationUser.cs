using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Lextatico.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; private set; }
        public virtual ICollection<Analyzer> Analyzers { get; } = new List<Analyzer>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();
    }
}
