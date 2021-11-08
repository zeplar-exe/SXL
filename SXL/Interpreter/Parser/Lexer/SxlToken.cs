using Jammo.ParserTools;

namespace SXL.Interpreter.Parser.Lexer
{
    public class SxlToken
    {
        public readonly string Text;
        public readonly StringContext Context;
        public readonly SxlTokenId Id;

        public SxlToken(string text, StringContext context, SxlTokenId id)
        {
            Text = text;
            Context = context;
            Id = id;
        }

        public bool Is(SxlTokenId id) => Id == id;

        public override string ToString()
        {
            return Text;
        }
    }
}