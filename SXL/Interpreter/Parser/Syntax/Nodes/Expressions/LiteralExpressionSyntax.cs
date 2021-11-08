using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        public SxlToken Literal { get; }
        
        public LiteralExpressionSyntax(SxlToken literal)
        {
            Literal = literal;
        }

        public override string ToString()
        {
            return Literal.ToString();
        }
    }
}