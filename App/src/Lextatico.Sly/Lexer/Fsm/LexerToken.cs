using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm
{
    public class LexerToken<T>
    {
        public LexerToken(T result, ReadOnlyMemory<char> value, LexerPosition position)
        {
            Result = result;
            SpanValue = value;
            Position = position;
        }

        public LexerToken()
        {
            IsEOS = true;
            End = true;
            Position = new LexerPosition(0, 0, 0);
        }

        private const char StringDelimiter = '"';

        private const char CharDelimiter = '\'';

        public ReadOnlyMemory<char> SpanValue { get; set; }

        public LexerPosition Position { get; set; }

        public int PositionInTokenFlow { get; set; }

        public bool IsEOS { get; set; }

        public bool IsEmpty { get; set; }

        public string Value => SpanValue.ToString();

        public string StringWithoutQuotes
        {
            get
            {
                var result = Value;
                if (StringDelimiter != (char)0)
                {
                    if (result.StartsWith(StringDelimiter.ToString())) result = result.Substring(1);
                    if (result.EndsWith(StringDelimiter.ToString())) result = result.Substring(0, result.Length - 1);
                }

                return result;
            }
        }

        public int IntValue => int.Parse(Value);

        public double DoubleValue => double.Parse(Value, CultureInfo.InvariantCulture);

        public char CharValue
        {
            get
            {
                var result = Value;
                if (CharDelimiter != (char)0)
                {
                    if (result.StartsWith(CharDelimiter.ToString()))
                    {
                        result = result.Substring(1);
                    }
                    if (result.EndsWith(CharDelimiter.ToString()))
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                }
                return result[0];
            }
        }


        public bool End { get; set; }
        public bool IsLineEnding { get; set; }
        public T Result { get; internal set; }

        public static LexerToken<T> Empty()
        {
            var empty = new LexerToken<T>();

            empty.IsEmpty = true;

            return empty;
        }

        public override string ToString()
        {
            if (IsEOS)
            {
                return "<<EOS>>";    
            }

            return $"{Result} (\"{Value}\") na {Position}";
        }
    }
}
