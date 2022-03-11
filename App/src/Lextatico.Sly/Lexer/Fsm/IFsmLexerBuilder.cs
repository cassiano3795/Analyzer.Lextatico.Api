using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Lexer
{
    public interface IFsmLexerBuilder<T>
        where T : struct
    {
        BuildResult<AbstractFsmLexer<T>> BuildLexer();

        FsmLexerBuilder<T> GoTo(int state);

        FsmLexerBuilder<T> GoTo(string mark);

        FsmLexerBuilder<T> Mark(string mark);

        FsmLexerNode<T> GetNode(int nodeId);

        FsmLexerNode<T> GetNode(string mark);

        FsmLexerBuilder<T> End(Token token, T nodeValue, bool isLineEnding = false);

        FsmLexerBuilder<T> SafeTransition(char input);

        FsmLexerBuilder<T> Transition(char input);

        FsmLexerBuilder<T> Transition(char[] inputs);

        FsmLexerBuilder<T> ConstantTransition(string constant);

        FsmLexerBuilder<T> RangeTransition(char start, char end);

        FsmLexerBuilder<T> AnyTransition();

        FsmLexerBuilder<T> TransitionTo(char input, int toNode);

        FsmLexerBuilder<T> TransitionTo(char[] inputs, int toNode);

        FsmLexerBuilder<T> RangeTransitionTo(char start, char end, int toNode);

        FsmLexerBuilder<T> AnyTransitionTo(int toNode);

        FsmLexerBuilder<T> TransitionTo(char input, string toNodeMark);

        FsmLexerBuilder<T> TransitionTo(char[] inputs, string toNodeMark);

        FsmLexerBuilder<T> RangeTransitionTo(char start, char end, string toNodeMark);

        FsmLexerBuilder<T> AnyTransitionTo(string toNodeMark);
    }
}