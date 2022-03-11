using System.Collections.Generic;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Parser.Builder
{
    public interface ILextaticoParserBuilder<T> where T : Token
    {
        BuildResult<Parser<T>> BuildParser(string rootRule, ParserType parserType, IEnumerable<string> productionRules);

        ParserConfiguration<T> ExtractParserConfiguration(IEnumerable<string> productionRules);
    }
}