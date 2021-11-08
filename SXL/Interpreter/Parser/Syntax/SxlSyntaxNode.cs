using System.Collections.Generic;
using System.Linq;

namespace SXL.Interpreter.Parser.Syntax
{
    public abstract class SxlSyntaxNode
    {
        private readonly List<SxlSyntaxNode> nodes = new();
        private readonly List<ParserError> errors = new();
        
        public IEnumerable<SxlSyntaxNode> Nodes => nodes.AsReadOnly();
        public IEnumerable<ParserError> Errors => errors.AsReadOnly();

        public bool HasError => errors.Any();

        public void AddNode(SxlSyntaxNode node)
        {
            nodes.Add(node);
        }
        
        public IEnumerable<SxlSyntaxNode> Descendents()
        {
            foreach (var node in Nodes)
            {
                yield return node;

                foreach (var nestedNode in node.Nodes)
                    yield return nestedNode;
            }
        }

        protected void ReportError(ParserError error)
        {
            errors.Add(error);
        }
    }
}