using SXL.Sandboxing;

namespace SXL.Interpreter
{
    public class SxlInterpreter
    {
        public ISandbox Sandbox { get; }
        
        public SxlInterpreter(ISandbox sandbox = null)
        {
            Sandbox = sandbox ?? new Sandbox();
        }
    }
}