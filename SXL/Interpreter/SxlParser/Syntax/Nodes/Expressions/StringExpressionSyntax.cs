using Jammo.ParserTools;
using SXL.Interpreter.SxlParser.SxlLexer;

namespace SXL.Interpreter.SxlParser.Syntax.Nodes.Expressions
{
    public class StringExpressionSyntax : ExpressionSyntax
    {
        public TelToken Literal;

        public new static StringExpressionSyntax Parse(EnumerableNavigator<TelToken> navigator)
        {
            return new StringExpressionSyntax { Literal = navigator.Current };
        }
        
        public override string ToString()
        {
            return Literal.ToString();
        }
    }
}