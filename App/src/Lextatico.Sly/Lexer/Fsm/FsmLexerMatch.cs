using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm
{
    public class FsmLexerMatch<T>
        where T : struct
    {
        public Dictionary<string, object> Properties { get; }

        public bool IsString { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsEOS { get; }

        public LexerToken<T> Result { get; set; }

        public int NodeId { get; }

        public LexerPosition NewPosition { get; set; }

        public bool IsLineEnding { get; set; }
        public T Value { get; }
        public ReadOnlyMemory<char> CurrentValue { get; }
        public LexerPosition Position { get; }
        public int Id { get; }
        public LexerPosition LexerPosition { get; }

        public FsmLexerMatch(bool success)
        {
            IsSuccess = success;
            IsEOS = !success;
        }

        protected FsmLexerMatch()
        {
            Properties = new Dictionary<string, object>();
        }

        public FsmLexerMatch(bool success, T result, Token token, string value, LexerPosition position, int nodeId, LexerPosition newPosition, bool isLineEnding)
            : this(success, result, token, new ReadOnlyMemory<char>(value.ToCharArray()), position, nodeId, newPosition, isLineEnding)
        { }

        public FsmLexerMatch(bool success, T result, Token token, ReadOnlyMemory<char> value, LexerPosition position, int nodeId,
            LexerPosition newPosition, bool isLineEnding)
        {
            Properties = new Dictionary<string, object> { { nameof(token), token } };
            IsSuccess = success;
            NodeId = nodeId;
            IsEOS = false;
            Result = new LexerToken<T>(result, value, position);
            NewPosition = newPosition;
            IsLineEnding = isLineEnding;
        }
    }
}