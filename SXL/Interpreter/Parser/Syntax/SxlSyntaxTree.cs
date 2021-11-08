using System.Collections.Generic;

namespace SXL.Interpreter.Parser.Syntax
{
    public class SxlSyntaxTree
    {
        public readonly SxlSyntaxNode Root;

        public SxlSyntaxTree(SxlSyntaxNode root)
        {
            Root = root;
        }

        public IEnumerable<SxlSyntaxNode> Enumerate()
        {
            yield return Root;

            foreach (var child in Root.Descendents())
                yield return child;
        }
    }
}