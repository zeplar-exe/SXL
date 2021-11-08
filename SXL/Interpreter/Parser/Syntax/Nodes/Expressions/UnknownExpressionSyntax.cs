using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class UnknownExpressionSyntax : ExpressionSyntax
    {
        public SxlToken Token;

        public UnknownExpressionSyntax(SxlToken token)
        {
            Token = token;
        }
        
        public override string ToString()
        {
            return Token.ToString();
        }
    }
}