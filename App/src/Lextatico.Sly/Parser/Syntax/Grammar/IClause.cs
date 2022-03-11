using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Parser.Syntax.Grammar
{
    public interface IClause<T> : GrammarNode<T> where T : Token
    {
        bool MayBeEmpty();
    }
}