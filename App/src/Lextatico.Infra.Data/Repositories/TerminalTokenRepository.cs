using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Domain.Models;
using Lextatico.Infra.Data.Context;

namespace Lextatico.Infra.Data.Repositories
{
    public class TerminalTokenRepository : BaseRepository<TerminalToken>, ITerminalTokenRepository
    {
        public TerminalTokenRepository(LextaticoContext lextaticoContext)
            : base(lextaticoContext)
        {
        }
    }
}
