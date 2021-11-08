using System.Collections.Generic;
using System.Linq;
using Jammo.ParserTools;

namespace SXL.Interpreter.Parser.Lexer
{
    public class SxlLexer
    {
        private readonly string text;
        private EnumerableNavigator<LexerToken> navigator;

        private StringContext context;

        private readonly List<SxlToken> tokens = new();
        private readonly List<LexerError> errors = new();

        public SxlLexer(string text)
        {
            this.text = text;
        }

        public IEnumerable<SxlToken> Tokens => tokens.AsReadOnly();
        public IEnumerable<LexerError> Errors => errors.AsReadOnly();

        public IEnumerable<SxlToken> Lex()
        {
            context = new StringContext();
            
            errors.Clear();
            
            navigator = new Jammo.ParserTools.Lexer(text, new LexerOptions(t => t.Is(LexerTokenId.Whitespace))
                {
                    IncludeUnderscoreAsAlphabetic = true,
                    IncludePeriodAsNumeric = true
                }).ToNavigator();

            foreach (var token in navigator.EnumerateFromIndex())
            {
                context.MoveColumn(token.Span.Size);

                switch (token.Id)
                {
                    case LexerTokenId.Alphabetic:
                    {
                        switch (token.RawToken)
                        {
                            case "define":
                                AddToken(token.ToString(), SxlTokenId.DefineInstruction);
                                break;
                            case "as":
                                AddToken(token.ToString(), SxlTokenId.AsInstruction);
                                break;
                            case "import":
                                AddToken(token.ToString(), SxlTokenId.ImportInstruction);
                                break;
                            case "from":
                                AddToken(token.ToString(), SxlTokenId.FromInstruction);
                                break;
                            case "protocol":
                                AddToken(token.ToString(), SxlTokenId.ProtocolKeyword);
                                break;
                            case "and":
                                AddToken(token.ToString(), SxlTokenId.And);
                                break;
                            case "or":
                                AddToken(token.ToString(), SxlTokenId.Or);
                                break;
                            case "not":
                                AddToken(token.ToString(), SxlTokenId.Not);
                                break;
                            default:
                                AddToken(token.ToString(), SxlTokenId.Identifier);
                                break;
                        }

                        break;
                    }
                    case LexerTokenId.Numeric:
                    {
                        AddToken(token.ToString(), SxlTokenId.NumericLiteral);
                        break;
                    }
                    case LexerTokenId.Quote:
                    case LexerTokenId.DoubleQuote:
                    {
                        if (LastIs(LexerTokenId.Backslash))
                        {
                            ReportError(token);
                            break;
                        }

                        var stringTokens = new List<LexerToken> { token };
                        
                        foreach (var literalToken in navigator.EnumerateFromIndex())
                        {
                            stringTokens.Add(literalToken);

                            if (literalToken.Is(token.Id) && !LastIs(LexerTokenId.Backslash))
                            { // Passing the id guarantees the closing token will be the same
                                AddToken(
                                    string.Concat(stringTokens.SelectMany(t => t.RawToken)),
                                    SxlTokenId.StringLiteral);

                                break;
                            }
                        }

                        break;
                    }
                    case LexerTokenId.LeftParenthesis:
                    {
                        AddToken(token.ToString(), SxlTokenId.OpenParenthesis);

                        break;
                    }
                    case LexerTokenId.RightParenthesis:
                    {
                        AddToken(token.ToString(), SxlTokenId.CloseParenthesis);
                        break;
                    }
                    case LexerTokenId.OpenBracket:
                    {
                        AddToken(token.ToString(), SxlTokenId.OpenBracket);
                        break;
                    }
                    case LexerTokenId.CloseBracket:
                    {
                        AddToken(token.ToString(), SxlTokenId.CloseBracket);
                        break;
                    }
                    case LexerTokenId.OpenCurlyBracket:
                    {
                        AddToken(token.ToString(), SxlTokenId.OpenCurlyBracket);
                        break;
                    }
                    case LexerTokenId.CloseCurlyBracket:
                    {
                        AddToken(token.ToString(), SxlTokenId.CloseCurlyBracket);
                        break;
                    }
                    case LexerTokenId.Newline:
                    {
                        context.MoveLine();
                        
                        break;
                    }
                    case LexerTokenId.Period:
                    {
                        AddToken(token.ToString(), SxlTokenId.Period);
                        break;
                    }
                    case LexerTokenId.Comma:
                    {
                        AddToken(token.ToString(), SxlTokenId.Comma);
                        break;
                    }
                    case LexerTokenId.Plus:
                    {
                        AddToken(token.ToString(), SxlTokenId.Plus);
                        break;
                    }
                    case LexerTokenId.Dash:
                    {
                        AddToken(token.ToString(), SxlTokenId.Minus);
                        break;
                    }
                    case LexerTokenId.Star:
                    {
                        AddToken(token.ToString(), SxlTokenId.Multiply);
                        break;
                    }
                    case LexerTokenId.Slash:
                    {
                        AddToken(token.ToString(), SxlTokenId.Divide);
                        break;
                    }
                    case LexerTokenId.LessThan:
                    {
                        if (NextIs(LexerTokenId.Equals))
                        {
                            navigator.Skip();

                            AddToken(token.ToString(), SxlTokenId.LessThanOrEqual);
                            break;
                        }

                        AddToken(token.ToString(), SxlTokenId.LessThan);
                        break;
                    }
                    case LexerTokenId.GreaterThan:
                    {
                        if (NextIs(LexerTokenId.Equals))
                        {
                            navigator.Skip();

                            AddToken(token.ToString(), SxlTokenId.GreaterThanOrEqual);
                            break;
                        }

                        AddToken(token.ToString(), SxlTokenId.GreaterThan);
                        break;
                    }
                    case LexerTokenId.Equals:
                    {
                        if (NextIs(LexerTokenId.Equals))
                        {
                            navigator.Skip();

                            AddToken(token.ToString(), SxlTokenId.Equals);
                            break;
                        }

                        if (NextIs(LexerTokenId.GreaterThan))
                        {
                            navigator.Skip();

                            AddToken(token.ToString(), SxlTokenId.DelegateOperator);
                            break;
                        }

                        AddToken(token.ToString(), SxlTokenId.Assignment);
                        break;
                    }
                    case LexerTokenId.ExclamationMark:
                    {
                        if (NextIs(LexerTokenId.Equals))
                        {
                            navigator.Skip();

                            AddToken(token.ToString(), SxlTokenId.NotEqual);
                            break;
                        }

                        ReportError(token);
                        break;
                    }
                    default:
                    {
                        ReportError(token);
                        AddToken(token.ToString(), SxlTokenId.Unknown);
                        break;
                    }
                }
            }

            return tokens;
        }

        private bool NextIs(LexerTokenId id)
        {
            return navigator.TakeIf(t=> t.Is(id), out _);
        }
        
        private bool LastIs(LexerTokenId id)
        {
            return navigator.TryPeekLast(out var token) && token.Is(id);
        }

        private void AddToken(string rawText, SxlTokenId id)
        {
            tokens.Add(new SxlToken(rawText, context, id));
        }

        private void ReportError(LexerToken token)
        {
            errors.Add(new LexerError("Unexpected character", token.ToString(), context));
        }
    }
}