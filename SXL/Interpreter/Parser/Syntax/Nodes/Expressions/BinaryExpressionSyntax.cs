using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class BinaryExpressionSyntax : ExpressionSyntax
    {
        public ExpressionSyntax Left;
        public SxlToken Operator;
        public ExpressionSyntax Right;
        
        public BinaryExpressionSyntax()
        {
            
        }
        
        public override string ToString()
        {
            return $"{Left?.ToString() ?? ""} {Operator?.ToString() ?? ""} {Right?.ToString() ?? ""}";
        }
    }
}