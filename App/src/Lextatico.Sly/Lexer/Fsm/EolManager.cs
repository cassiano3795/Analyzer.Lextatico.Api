using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Extensions;

namespace Lextatico.Sly.Lexer.Fsm
{
    public class EolManager
    {
        public static ReadOnlyMemory<char> GetToEndOfLine(ReadOnlyMemory<char> value, int position)
        {
            var CurrentPosition = position;
            var spanValue = value.Span;
            var current = spanValue[CurrentPosition];
            var end = IsEndOfLine(value, CurrentPosition);
            while (CurrentPosition < value.Length && end == EolType.No)
            {
                CurrentPosition++;
                end = IsEndOfLine(value, CurrentPosition);
            }

            return value.Slice(position, CurrentPosition - position + (end == EolType.Windows ? 2 : 1));
        }

        public static EolType IsEndOfLine(ReadOnlyMemory<char> value, int position)
        {
            var end = EolType.No;
            var n = value.At(position);
            if (n == '\n')
            {
                end = EolType.Nix;
            }
            else if (n == '\r')
            {
                if (value.At(position + 1) == '\n')
                    end = EolType.Windows;
                else
                    end = EolType.Mac;
            }

            return end;
        }

        public static List<int> GetLinesLength(ReadOnlyMemory<char> value)
        {
            var lineLengths = new List<int>();
            var lines = new List<string>();
            var previousStart = 0;
            var i = 0;
            while (i < value.Length)
            {
                var end = IsEndOfLine(value, i);
                if (end != EolType.No)
                {
                    if (end == EolType.Windows) i ++;
                    var line = value.Slice(previousStart, i - previousStart);
                    lineLengths.Add(line.Length);
                    lines.Add(line.ToString());
                    previousStart = i + 1;
                }

                i++;
            }

            lineLengths.Add(value.Slice(previousStart, i - previousStart).Length);
            return lineLengths;
        }
    }

    public enum EolType
    {
        Windows,
        Nix,

        Mac,
        Environment,

        No
    }
}