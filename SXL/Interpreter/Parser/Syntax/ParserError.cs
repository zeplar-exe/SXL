namespace SXL.Interpreter.Parser.Syntax
{
    public class ParserError
    {
        public readonly string Message;

        public ParserError(string message)
        {
            Message = message;
        }
    }
}