using Jammo.ParserTools;

namespace SXL.Interpreter.SxlParser.SxlLexer
{
    public readonly struct LexerError
    {
        public readonly string Message;
        public readonly string Text;
        public readonly StringContext Context;

        public LexerError(string message, string text, StringContext context)
        {
            Message = message;
            Text = text;
            Context = context;
        }
    }
}