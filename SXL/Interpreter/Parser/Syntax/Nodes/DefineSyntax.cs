using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax.Nodes
{
    public class DefineSyntax : SxlSyntaxNode
    {
        public SxlToken DefineToken { get; }
        public SxlIdentifier Identifier;
        public AsSyntax Initializer;

        internal DefineSyntax(SxlToken defineToken)
        {
            DefineToken = defineToken;
        }
    }
}