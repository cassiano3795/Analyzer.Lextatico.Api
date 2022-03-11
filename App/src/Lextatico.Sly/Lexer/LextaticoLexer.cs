using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer.Fsm;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Lexer
{
    public class LextaticoLexer<T> : ILexer<T>
        where T : Token
    {
        public LextaticoLexer(IList<T> tokens) : base()
        {
            DerivedTokens = new Dictionary<TokenType, Dictionary<string, T>>();
            _tokens = tokens;
        }

        private readonly IList<T> _tokens;

        public FsmLexerBuilder<TokenType> FsmLexerBuilder;

        protected AbstractFsmLexer<TokenType> FsmLexer;

        protected readonly Dictionary<TokenType, Dictionary<string, T>> DerivedTokens;

        protected int StringCounter;

        protected int CharCounter;

        protected char StringDelimiterChar;

        protected char EscapeStringDelimiterChar;

        public void AddDefinition(TokenDefinition tokenDefinition) { }

        public LexerResult<T> Tokenize(string source)
        {
            var memorySource = new ReadOnlyMemory<char>(source.ToCharArray());

            return Tokenize(memorySource);
        }

        public LexerResult<T> Tokenize(ReadOnlyMemory<char> source)
        {
            var position = new LexerPosition();

            var tokens = new List<LexerToken<T>>();

            var fsmMatch = FsmLexer.Run(source, new LexerPosition());

            if (!fsmMatch.IsSuccess && !fsmMatch.IsEOS)
            {
                var result = fsmMatch.Result;
                var error = new LexicalError(result.Position.Line, result.Position.Column, result.Value);
                return new LexerResult<T>(error);
            }
            else if (fsmMatch.IsSuccess)
            {
                position = fsmMatch.NewPosition;
            }

            while (fsmMatch.IsSuccess)
            {
                var lexerResult = new LexerResult<T>();

                var transcoded = Transcode(fsmMatch, lexerResult);

                if (lexerResult.IsError)
                    return lexerResult;

                tokens.Add(transcoded);

                fsmMatch = FsmLexer.Run(source, position);

                if (!fsmMatch.IsSuccess && !fsmMatch.IsEOS)
                {
                    var result = fsmMatch.Result;

                    var error = new LexicalError(result.Position.Line, result.Position.Column, result.Value);

                    return new LexerResult<T>(error);
                }

                position = fsmMatch.NewPosition;
            }

            var eos = new LexerToken<T>();

            var prev = tokens.LastOrDefault();

            if (prev == null)
            {
                eos.Position = new LexerPosition(1, 1, 1);
            }
            else
            {
                eos.Position = new LexerPosition(prev.Position.Index + 1, prev.Position.Line,
                    prev.Position.Column + prev.Value.Length);
            }

            eos.Result = new Token("Fim do arquivo", "EOS", "Representa o fim do arquivo", "<<EOS>>", TokenType.Default, null) as T;

            tokens.Add(eos);

            return new LexerResult<T>(tokens);
        }

        public LextaticoLexer<T> InitializeLexer(BuildResult<ILexer<T>> buildResult)
        {
            FsmLexerBuilder = new FsmLexerBuilder<TokenType>();

            FsmLexerBuilder
                .IgnoreWS()
                .IgnoreEol()
                .WhiteSpace(new[] { ' ', '\t' });

            FsmLexerBuilder.Mark("start");

            var identifierToken = _tokens.FirstOrDefault(t => t.TokenType == TokenType.Identifier);

            identifierToken ??=
                (T)new Token("Default", "id", "", "", TokenType.Identifier, IdentifierType.Alpha);

            if (_tokens.Any(a => a.TokenType == TokenType.Identifier || a.TokenType == TokenType.KeyWord))
            {
                AddIdentifier(identifierToken, identifierToken?.IdentifierType ?? IdentifierType.Alpha);
            }

            if (_tokens.Any(a => a.TokenType == TokenType.Integer || a.TokenType == TokenType.Float))
            {
                var integerToken = _tokens.FirstOrDefault(f => f.TokenType == TokenType.Integer);

                AddInteger(integerToken);

                if (_tokens.Any(a => a.TokenType == TokenType.Float))
                {
                    var floatToken = _tokens.FirstOrDefault(f => f.TokenType == TokenType.Float);

                    AddFloat(floatToken);
                }
            }

            foreach (var token in _tokens)
            {
                AddLexeme(token, buildResult);
            }

            FsmLexer = FsmLexerBuilder.Lexer;

            return this;
        }

        public virtual LexerToken<T> Transcode(FsmLexerMatch<TokenType> fsmMatch, LexerResult<T> lexerResult)
        {
            var token = DecodeToken(fsmMatch, lexerResult);

            var lexerToken = new LexerToken<T>();
            var innerLexerToken = fsmMatch.Result;
            lexerToken.IsEmpty = innerLexerToken.IsEmpty;
            lexerToken.SpanValue = innerLexerToken.SpanValue;
            lexerToken.Position = innerLexerToken.Position;
            lexerToken.IsLineEnding = fsmMatch.IsLineEnding;
            lexerToken.IsEOS = fsmMatch.IsEOS;
            lexerToken.Result = token;

            return lexerToken;
        }

        private T DecodeToken(FsmLexerMatch<TokenType> fsmMatch, LexerResult<T> lexerResult)
        {
            var token = (T)fsmMatch.Properties["token"];

            var result = fsmMatch.Result;

            if (token.TokenType != TokenType.Identifier)
                return token;

            if (DerivedTokens.ContainsKey(TokenType.Identifier))
            {
                var possibleTokens = DerivedTokens[TokenType.Identifier];

                if (possibleTokens.ContainsKey(result.Value))
                    return possibleTokens[result.Value];

                if (possibleTokens.ContainsKey(TokenType.Identifier.ToString().ToLower()))
                    return token;
            }

            var error = new LexicalError(result.Position.Line, result.Position.Column, result.Value);

            lexerResult.Error = error;

            return default(T);
        }

        private void AddLexeme(T token, BuildResult<ILexer<T>> buildResult)
        {
            switch (token.TokenType)
            {
                case TokenType.Identifier:
                    var nameIdentifier = TokenType.Identifier.ToString().ToLower();
                    AddLexeme(token, nameIdentifier);
                    break;
                case TokenType.KeyWord:
                    AddKeyWord(token);
                    break;
                case TokenType.SugarToken:
                    AddSugarToken(token, buildResult, token.Lexeme);
                    break;
                case TokenType.String:
                    AddString(token, buildResult);
                    break;
                case TokenType.Char:
                    AddChar(token, buildResult);
                    break;
                default:
                    break;
            }
        }

        private void AddLexeme(T token, string keyWord)
        {
            if (!DerivedTokens.TryGetValue(TokenType.Identifier, out var derivedToken))
            {
                // if (genericToken == GenericToken.Identifier)
                // {
                //     tokensForGeneric = new Dictionary<string, IN>(KeyWordComparer);
                // }
                // else
                // {
                derivedToken = new Dictionary<string, T>();
                // }

                DerivedTokens[TokenType.Identifier] = derivedToken;
            }

            derivedToken[keyWord] = token;
        }

        private void AddIdentifier(Token token, IdentifierType identifierType)
        {
            var tokenType = token?.TokenType ?? TokenType.Identifier;

            var nameIdentifier = TokenType.Identifier.ToString();

            FsmLexerBuilder
                    .GoTo("start")
                    .RangeTransition('a', 'z')
                    .Mark(nameIdentifier)
                    .GoTo("start")
                    .RangeTransitionTo('A', 'Z', nameIdentifier)
                    .RangeTransitionTo('a', 'z', nameIdentifier)
                    .RangeTransitionTo('A', 'Z', nameIdentifier)
                    .End(token, tokenType);

            if (identifierType == IdentifierType.AlphaNum || identifierType == IdentifierType.AlphaNumDash)
            {
                FsmLexerBuilder
                    .GoTo(nameIdentifier)
                    .RangeTransitionTo('0', '9', nameIdentifier);
            }

            if (identifierType == IdentifierType.AlphaNumDash)
            {
                FsmLexerBuilder
                    .GoTo("start")
                    .TransitionTo('_', nameIdentifier)
                    .TransitionTo('_', nameIdentifier)
                    .TransitionTo('-', nameIdentifier);
            }
        }

        private void AddString(T token, BuildResult<ILexer<T>> buildResult)
        {
            var stringDelimiter = "\"";

            var escapeDelimiterChar = "\\";

            // if (string.IsNullOrEmpty(stringDelimiter) || stringDelimiter.Length > 1)
            //     buildResult.AddError(new LexerInitializationError(ErrorLevel.Fatal,
            //         string.Format("bad lexem {0} : StringToken lexeme delimiter char <{1}> must be 1 character length.", stringDelimiter, token.ToString()),
            //         ErrorCodes.LexerStringDelimiterMustBe1Char));

            // if (stringDelimiter.Length == 1 && char.IsLetterOrDigit(stringDelimiter[0]))
            //     buildResult.AddError(new InitializationError(ErrorLevel.Fatal,
            //         string.Format("bad lexem {0} : StringToken lexeme delimiter char <{1}> can not start with a letter.", stringDelimiter, token.ToString()),
            //         ErrorCodes.LexerStringDelimiterCannotBeLetterOrDigit));

            // if (string.IsNullOrEmpty(escapeDelimiterChar) || escapeDelimiterChar.Length > 1)
            //     buildResult.AddError(new InitializationError(ErrorLevel.Fatal,
            //         I18N.Instance.GetText(I18n, Message.StringEscapeCharMustBe1Char, escapeDelimiterChar, token.ToString()),
            //         ErrorCodes.LexerS EXER_STRING_ESCAPE_CHAR_MUST_BE_1_CHAR));

            // if (escapeDelimiterChar.Length == 1 && char.IsLetterOrDigit(escapeDelimiterChar[0]))
            //     buildResult.AddError(new InitializationError(ErrorLevel.Fatal,
            //         I18N.Instance.GetText(I18n, Message.StringEscapeCharCannotBeLetterOrDigit, escapeDelimiterChar, token.ToString()),
            //         ErrorCodes.LEXER_STRING_ESCAPE_CHAR_CANNOT_BE_LETTER_OR_DIGIT));

            StringDelimiterChar = (char)0;
            var stringDelimiterChar = (char)0;

            EscapeStringDelimiterChar = (char)0;
            var escapeStringDelimiterChar = (char)0;

            if (stringDelimiter.Length == 1)
            {
                StringCounter++;

                StringDelimiterChar = stringDelimiter[0];
                stringDelimiterChar = stringDelimiter[0];

                EscapeStringDelimiterChar = escapeDelimiterChar[0];
                escapeStringDelimiterChar = escapeDelimiterChar[0];
            }

            if (stringDelimiterChar != escapeStringDelimiterChar)
            {
                var name = $"{TokenType.String.ToString()}";

                var nameEscape = $"escape_{name}";

                var nameEnd = $"{name}_end";

                FsmLexerBuilder.GoTo("start");

                FsmLexerBuilder.Transition(stringDelimiterChar)
                    .Mark(name + StringCounter)
                    .ExceptTransitionTo(new[] { stringDelimiterChar, escapeStringDelimiterChar },
                        name + StringCounter)
                    .Transition(escapeStringDelimiterChar)
                    .Mark(nameEscape + StringCounter)
                    .ExceptTransitionTo(new[] { stringDelimiterChar }, name + StringCounter)
                    .GoTo(nameEscape + StringCounter)
                    .TransitionTo(stringDelimiterChar, name + StringCounter)
                    .Transition(stringDelimiterChar)
                    .End(token, TokenType.String)
                    .Mark(nameEnd + StringCounter);

                FsmLexerBuilder.Lexer.StringDelimiter = stringDelimiterChar;
            }
            else
            {
                var exceptDelimiter = new[] { stringDelimiterChar };
                var in_string = "in_string_same";
                var escaped = "escaped_same";
                var delim = "delim_same";

                FsmLexerBuilder.GoTo("start")
                    .Transition(stringDelimiterChar)
                    .Mark(in_string + StringCounter)
                    .ExceptTransitionTo(exceptDelimiter, in_string + StringCounter)
                    .Transition(stringDelimiterChar)
                    .Mark(escaped + StringCounter)
                    .End(token, TokenType.String)
                    .Transition(stringDelimiterChar)
                    .Mark(delim + StringCounter)
                    .ExceptTransitionTo(exceptDelimiter, in_string + StringCounter);

                FsmLexerBuilder.GoTo(delim + StringCounter)
                    .TransitionTo(stringDelimiterChar, escaped + StringCounter)
                    .ExceptTransitionTo(exceptDelimiter, in_string + StringCounter);
            }
        }

        private void AddChar(T token, BuildResult<ILexer<T>> buildResult)
        {
            var charDelimiter = "'";

            var escapeDelimiterChar = "\\";

            // if (string.IsNullOrEmpty(charDelimiter) || charDelimiter.Length > 1)
            //    result.AddError(new InitializationError(ErrorLevel.FATAL,
            //        I18N.Instance.GetText(I18n,Message.CharDelimiterMustBe1Char,charDelimiter,token.ToString()),
            //         ErrorCodes.LEXER_CHAR_DELIMITER_MUST_BE_1_CHAR));

            // if (charDelimiter.Length == 1 && char.IsLetterOrDigit(charDelimiter[0]))
            //     result.AddError(new InitializationError(ErrorLevel.FATAL,
            //         I18N.Instance.GetText(I18n, Message.CharDelimiterCannotBeLetter,charDelimiter,token.ToString()), 
            //         ErrorCodes.LEXER_CHAR_DELIMITER_CANNOT_BE_LETTER));

            // if (string.IsNullOrEmpty(escapeDelimiterChar) || escapeDelimiterChar.Length > 1)
            //     result.AddError(new InitializationError(ErrorLevel.FATAL,
            //         I18N.Instance.GetText(I18n,Message.CharEscapeCharMustBe1Char,escapeDelimiterChar,token.ToString()),
            //         ErrorCodes.LEXER_CHAR_ESCAPE_CHAR_MUST_BE_1_CHAR));
            // if (escapeDelimiterChar.Length == 1 && char.IsLetterOrDigit(escapeDelimiterChar[0]))
            //     result.AddError(new InitializationError(ErrorLevel.FATAL,
            //         I18N.Instance.GetText(I18n,Message.CharEscapeCharCannotBeLetterOrDigit,escapeDelimiterChar,token.ToString()),
            //         ErrorCodes.LEXER_CHAR_ESCAPE_CHAR_CANNOT_BE_LETTER_OR_DIGIT));

            CharCounter++;

            var charDelimiterChar = charDelimiter[0];

            var escapeChar = escapeDelimiterChar[0];

            FsmLexerBuilder.GoTo("start");

            var name = $"{TokenType.Char.ToString()}";

            var nameStart = $"start_{name}";

            var nameEnd = $"end_{name}";

            var nameEscape = $"escapeChar_{name}";

            var nameUnicode = $"unicode_{name}";

            FsmLexerBuilder.Transition(charDelimiterChar)
                .Mark(nameStart + "_" + CharCounter)
                .ExceptTransition(new[] { charDelimiterChar, escapeChar })
                .Mark(name + "_" + CharCounter)
                .Transition(charDelimiterChar)
                .Mark(nameEnd + "_" + CharCounter)
                .End(token, TokenType.Char)
                .GoTo(nameStart + "_" + CharCounter)
                .Transition(escapeChar)
                .Mark(nameEscape + "_" + CharCounter)
                .ExceptTransitionTo(new[] { 'u' }, name + "_" + CharCounter);

            FsmLexerBuilder.Lexer.StringDelimiter = charDelimiterChar;

            // unicode transitions ?
            FsmLexerBuilder = FsmLexerBuilder.GoTo(nameEscape + "_" + CharCounter)
            .Transition('u')
            .Mark(nameUnicode + "_" + CharCounter)
            .RepetitionTransitionTo(name + "_" + CharCounter, 4, "[0-9,a-z,A-Z]");
        }

        private void AddInteger(Token token, TokenType tokenType = TokenType.Integer)
        {
            var name = TokenType.Integer.ToString();

            FsmLexerBuilder.GoTo("start")
                .RangeTransition('0', '9')
                .Mark(name)
                .RangeTransitionTo('0', '9', name)
                .End(token, tokenType);
        }

        private void AddFloat(Token token, TokenType tokenType = TokenType.Float)
        {
            var name = TokenType.Float.ToString();

            var startName = $"start_{name}";

            FsmLexerBuilder.Transition('.')
                .Mark(startName)
                .RangeTransition('0', '9')
                .Mark(name)
                .RangeTransitionTo('0', '9', name)
                .End(token, tokenType);
        }

        private void AddKeyWord(T token)
        {
            AddLexeme(token, token.Lexeme);
        }

        private void AddSugarToken(T token, BuildResult<ILexer<T>> buildResult, string sugarToken)
        {
            if (char.IsLetter(sugarToken[0]))
            {
                buildResult.AddError(new InitializationError(ErrorLevel.Fatal,
                    string.Format("bad lexem {0} : SugarToken lexeme <{1}> can not start with a letter.", sugarToken, token.ToString()),
                    ErrorCodes.LexerSugarTokenCannotStartWithLetter));
                return;
            }

            FsmLexerBuilder.GoTo("start");

            foreach (var c in sugarToken)
            {
                FsmLexerBuilder.SafeTransition(c);
            }

            FsmLexerBuilder.End(token, TokenType.SugarToken);
        }
    }
}
