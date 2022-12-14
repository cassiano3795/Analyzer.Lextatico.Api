using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analyzer.Lextatico.Sly.Result;

namespace Analyzer.Lextatico.Sly.Lexer
{
    public interface ILexer<T> where T : Token
    {
        void AddDefinition(TokenDefinition tokenDefinition);

        LexerResult<T> Tokenize(string source);

        LexerResult<T> Tokenize(ReadOnlyMemory<char> source);

        LextaticoLexer<T> InitializeLexer(BuildResult<ILexer<T>> buildResult);
    }
}
