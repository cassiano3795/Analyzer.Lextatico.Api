using System;

namespace Lextatico.Domain.Models
{
    public class AnalyzerTerminalToken : Base
    {
        public AnalyzerTerminalToken()
        {
        }

        public AnalyzerTerminalToken(Guid analyzerId, Guid terminalTokenId)
        {
            AnalyzerId = analyzerId;
            TerminalTokenId = terminalTokenId;
        }
        
        public Guid AnalyzerId { get; private set; }
        public void SetAnalyzerId(Guid analyzerId) => AnalyzerId = analyzerId;
        public virtual Analyzer Analyzer { get; private set; }
        public Guid TerminalTokenId { get; private set; }
        public void SetTerminalTokenId(Guid terminalTokenId) => TerminalTokenId = terminalTokenId;
        public virtual TerminalToken TerminalToken { get; private set; }
    }
}
