using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer.Fsm;

namespace Lextatico.Sly.Lexer
{
    public abstract class AbstractFsmLexer<T>
        where T : struct
    {
        public AbstractFsmLexer()
        {
            Nodes = new Dictionary<int, FsmLexerNode<T>>();
            Transitions = new Dictionary<int, List<FsmLexerTransition>>();
        }

        public char StringDelimiter = '"';

        public bool IgnoreWhiteSpace { get; set; }

        public bool IgnoreEol { get; set; }

        public List<char> WhiteSpaces { get; set; }

        protected readonly Dictionary<int, FsmLexerNode<T>> Nodes;

        protected readonly Dictionary<int, List<FsmLexerTransition>> Transitions;

        internal int NewNodeId => Nodes.Count;

        internal bool HasState(int state)
        {
            return Nodes.ContainsKey(state);
        }

        public abstract FsmLexerTransition GetTransition(int nodeId, char token);

        public abstract void AddTransition(FsmLexerTransition transition);

        public abstract FsmLexerNode<T> AddNode(T value);

        public abstract FsmLexerNode<T> AddNode();

        internal FsmLexerNode<T> GetNode(int state)
        {
            Nodes.TryGetValue(state, out var node);

            return node;
        }

        public abstract FsmLexerNode<T> GetNext(int from, char token);

        public abstract FsmLexerMatch<T> Run(string source, LexerPosition lexerPosition);

        public abstract FsmLexerMatch<T> Run(ReadOnlyMemory<char> source, LexerPosition lexerPosition);
    }
}