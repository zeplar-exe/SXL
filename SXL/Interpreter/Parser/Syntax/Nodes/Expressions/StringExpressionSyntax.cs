using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class StringExpressionSyntax : LiteralExpressionSyntax
    {
        public string Value => Literal.ToString();
        
        public StringExpressionSyntax(SxlToken literal) : base(literal)
        {
            
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
}