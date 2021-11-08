using System.Globalization;
using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes.Expressions
{
    public class NumericExpressionSyntax : LiteralExpressionSyntax
    {
        public NumericExpressionSyntax(SxlToken literal) : base(literal)
        {
            
        }

        public double Value
        {
            get
            {
                if (double.TryParse(Literal.Text, out var d))
                    return d;

                return 0;
            }
        }
        
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}