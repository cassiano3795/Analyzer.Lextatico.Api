using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Lexer
{
    public class LextaticoLexerBuilder<T> : ILexerBuilder<T> where T : Token
    {
        public LextaticoLexerBuilder(IList<T> tokens)
        {
            _tokens = tokens;
        }

        private readonly IList<T> _tokens;

        public BuildResult<ILexer<T>> Build()
        {
            var buildResult = new BuildResult<ILexer<T>>();

            var result = new LextaticoLexer<T>(_tokens).InitializeLexer(buildResult);

            buildResult.Result = result;

            return buildResult;
        }
    }
}