using SXL.Interpreter.Parser.Lexer;
using SXL.Interpreter.Parser.Syntax.Nodes.Expressions;

namespace SXL.Interpreter.Parser.Syntax.Nodes
{
    public class AsSyntax : SxlSyntaxNode
    {
        public SxlToken AsToken { get; }
        public ExpressionSyntax Value;

        public AsSyntax(SxlToken asToken)
        {
            AsToken = asToken;
        }
    }
}