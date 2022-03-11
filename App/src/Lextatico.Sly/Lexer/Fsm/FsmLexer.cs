using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Extensions;
using Lextatico.Sly.Lexer.Fsm;

namespace Lextatico.Sly.Lexer
{
    public class FsmLexer<T> : AbstractFsmLexer<T>
        where T : struct
    {
        public FsmLexer()
        {
            WhiteSpaces = new List<char>();
        }

        public override FsmLexerNode<T> AddNode(T value)
        {
            var lexerNode = new FsmLexerNode<T>(value);

            return AddNode(lexerNode);
        }

        public override FsmLexerNode<T> AddNode()
        {
            var node = new FsmLexerNode<T>();

            return AddNode(node);
        }

        private FsmLexerNode<T> AddNode(FsmLexerNode<T> lexerNode)
        {
            lexerNode.Id = Nodes.Count;

            Nodes[lexerNode.Id] = lexerNode;

            return lexerNode;
        }

        public override void AddTransition(FsmLexerTransition transition)
        {
            var transitions = new List<FsmLexerTransition>();

            if (Transitions.ContainsKey(transition.FromNode)) transitions = Transitions[transition.FromNode];

            transitions.Add(transition);

            Transitions[transition.FromNode] = transitions;
        }

        public override FsmLexerNode<T> GetNext(int from, char token)
        {
            var node = Nodes[from];

            return Move(node, token, "".AsMemory());
        }

        public override FsmLexerTransition GetTransition(int nodeId, char token)
        {
            FsmLexerTransition transition = null;

            if (HasState(nodeId))
                if (Transitions.ContainsKey(nodeId))
                {
                    var leavingTransitions = Transitions[nodeId];

                    transition = leavingTransitions.FirstOrDefault(t => t.Match(token));
                }

            return transition;
        }

        public override FsmLexerMatch<T> Run(string source, LexerPosition lexerPosition)
        {
            return Run(new ReadOnlyMemory<char>(source.ToCharArray()), lexerPosition);
        }

        public override FsmLexerMatch<T> Run(ReadOnlyMemory<char> source, LexerPosition lexerPosition)
        {
            ConsumeWhiteSpace(source, lexerPosition);

            if (lexerPosition.Index >= source.Length)
            {
                return new FsmLexerMatch<T>(false);
            }

            var position = lexerPosition.Clone();

            FsmLexerMatch<T> result = null;

            var currentNode = Nodes[0];

            while (lexerPosition.Index < source.Length)
            {
                var currentCharacter = source.At(lexerPosition);
                var currentValue = source.Slice(position.Index, lexerPosition.Index - position.Index + 1);
                currentNode = Move(currentNode, currentCharacter, currentValue);
                if (currentNode == null)
                {
                    // No more viable transitions, so exit loop
                    break;
                }

                if (currentNode.IsEnd)
                {
                    // Remember the possible match
                    result = new FsmLexerMatch<T>(true, currentNode.Value, currentNode.Token, currentValue, position, currentNode.Id, lexerPosition, currentNode.IsLineEnding);
                }

                lexerPosition.Index++;
                lexerPosition.Column++;
            }

            if (result != null)
            {
                // Backtrack
                var length = result.Result.Value.Length;
                lexerPosition.Index = result.Result.Position.Index + length;
                lexerPosition.Column = result.Result.Position.Column + length;

                return result;
            }

            if (lexerPosition.Index >= source.Length)
            {
                // Failed on last character, so need to backtrack
                lexerPosition.Index -= 1;
                lexerPosition.Column -= 1;
            }

            var errorChar = source.Slice(lexerPosition.Index, 1);

            result = new FsmLexerMatch<T>(false, default(T), new Token(), errorChar, lexerPosition, -1, lexerPosition, false);

            return result;
        }

        private FsmLexerNode<T> Move(FsmLexerNode<T> from, char token, ReadOnlyMemory<char> value)
        {
            if (from != null && Transitions.TryGetValue(from.Id, out var transitions))
            {
                for (var i = 0; i < transitions.Count; ++i)
                {
                    var transition = transitions[i];
                    if (transition.Match(token, value))
                    {
                        return Nodes[transition.ToNode];
                    }
                }
            }

            return null;
        }

        private void ConsumeWhiteSpace(ReadOnlyMemory<char> source, LexerPosition position)
        {

            while (position.Index < source.Length)
            {
                if (IgnoreWhiteSpace)
                {
                    var currentCharacter = source.At(position.Index);
                    if (WhiteSpaces.Contains(currentCharacter))
                    {
                        position.Index++;
                        position.Column++;
                        continue;
                    }
                }

                if (IgnoreEol)
                {
                    var eol = EolManager.IsEndOfLine(source, position.Index);
                    if (eol != EolType.No)
                    {
                        position.Index += eol == EolType.Windows ? 2 : 1;
                        position.Column = 1;
                        position.Line++;
                        continue;
                    }
                }

                break;
            }
        }
    }
}
