using Lextatico.Sly.Result;

namespace Lextatico.Sly.Lexer
{
    public interface ILexerBuilder<T> where T : Token
    {
        BuildResult<ILexer<T>> Build();
    }
}