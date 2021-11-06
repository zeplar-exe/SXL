using Jammo.ParserTools;
using SXL.Syntax;
using SXL.Syntax.Nodes;

namespace SXL
{
    public static class TelParser
    {
        public static TelSyntaxTree Parse(string text)
        {
            var root = new TelCompilationRoot();
            var state = new StateMachine<ParserState>();
            var navigator = new TelLexer(text).Lex().ToNavigator();
            
            foreach (var token in navigator.EnumerateFromIndex())
            {
                switch (state.Current)
                {
                    case ParserState.Any:
                    {
                        switch (token.Id)
                        {
                            case TelTokenId.DefineInstruction:
                            {
                                root.AddNode(DefineSyntax.Parse(navigator));
                                // TODO: Add parse methods
                                break;
                            }
                        }

                        break;
                    }
                }
            }

            return new TelSyntaxTree(root);
        }

        private enum ParserState
        {
            Any
        }
    }
}