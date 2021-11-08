using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser
{
    public readonly struct ParserDiagnostic
    {
        public string Message { get; }
        public SxlToken Token { get; }
        public Severity Severity { get; }

        public ParserDiagnostic(string message, SxlToken token, Severity severity)
        {
            Message = message;
            Token = token;
            Severity = severity;
        }
    }

    public enum Severity
    {
        Warning,
        Error
    }
}