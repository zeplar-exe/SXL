using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class KeywordExpressionSyntax : LiteralExpressionSyntax
    {
        public KeywordExpressionSyntax(SxlToken literal) : base(literal)
        {
            
        }
    }
}