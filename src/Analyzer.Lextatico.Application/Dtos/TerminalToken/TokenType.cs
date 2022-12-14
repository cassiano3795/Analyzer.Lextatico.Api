using System.ComponentModel;

namespace Analyzer.Lextatico.Application.Dtos.TerminalToken
{
    public enum TokenType
    {
        [Description("Quando nenhum TokenType é definido. Ex: node \"start\"")]
        Default,
        Identifier,
        String,
        Char,
        Integer,
        Float,
        KeyWord,
        SugarToken
    }
}
