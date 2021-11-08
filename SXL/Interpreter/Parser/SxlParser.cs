using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Jammo.ParserTools;
using SXL.Interpreter.Parser.Lexer;
using SXL.Interpreter.Parser.Syntax;
using SXL.Interpreter.Parser.Syntax.Nodes;
using SXL.Interpreter.Parser.Syntax.Nodes.Expressions;

namespace SXL.Interpreter.Parser
{
    public class SxlParser
    {
        private EnumerableNavigator<SxlToken> navigator;
        private readonly List<ParserDiagnostic> diagnostics = new();

        public IEnumerable<ParserDiagnostic> Diagnostics => diagnostics.AsReadOnly();

        public SxlParser(string text)
        {
            navigator = new SxlLexer(text).Lex().ToNavigator();
        }
        
        public SxlParser(Stream stream)
        {
            navigator = new SxlLexer(new StreamReader(stream).ReadToEnd()).Lex().ToNavigator();
        }

        public SxlSyntaxTree Parse()
        {
            var root = new SxlCompilationRoot();
            
            foreach (var token in navigator.EnumerateFromIndex())
            {
                switch (token.Id)
                {
                    case SxlTokenId.DefineInstruction:
                    {
                        root.AddNode(ParseDefineSyntax());
                        break;
                    }
                }

                break;
            }

            return new SxlSyntaxTree(root);
        }

        private DefineSyntax ParseDefineSyntax()
        {
            var syntax = new DefineSyntax(navigator.Current);

            while (navigator.TryMoveNext(out var token))
            {
                if (syntax.HasError)
                    break;

                switch (token.Id)
                {
                    case SxlTokenId.Identifier:
                        syntax.Identifier = new SxlIdentifier { Name = token.ToString() };

                        if (navigator.TakeIf(t => t.Is(SxlTokenId.AsInstruction), out _))
                            syntax.Initializer = ParseAsSyntax();

                        return syntax;
                    default:
                        ReportError("Expected an identifier.");
                        break;
                }
            }

            return syntax;
        }

        private AsSyntax ParseAsSyntax()
        {
            var syntax = new AsSyntax(navigator.Current);

            if (!navigator.TryMoveNext(out _))
                return syntax;
            
            syntax.Value = ParseExpression();

            return syntax;
        }
        
        private ExpressionSyntax ParseExpression()
        {
            switch (navigator.Current.Id)
            {
                case SxlTokenId.StringLiteral:
                    return ParseStringExpression();
                case SxlTokenId.NumericLiteral:
                    return ParseNumericExpression();
                default:
                    return new UnknownExpressionSyntax(navigator.Current);
            }
        }

        private StringExpressionSyntax ParseStringExpression()
        {
            return new StringExpressionSyntax(navigator.Current);
        }

        private NumericExpressionSyntax ParseNumericExpression()
        {
            return new NumericExpressionSyntax(navigator.Current);
        }

        private void ReportError(string message)
        {
            Report(message, Severity.Error);
        }
        
        private void ReportWarning(string message)
        {
            Report(message, Severity.Warning);
        }

        private void Report(string message, Severity severity)
        {
            diagnostics.Add(new ParserDiagnostic(message, navigator.Current, severity));
        }
    }
}