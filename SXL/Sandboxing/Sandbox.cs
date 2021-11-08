using SXL.Interpreter;
using SXL.Sandboxing.Permissions;

namespace SXL.Sandboxing
{
    public sealed class Sandbox : ISandbox
    {
        public SxlInterpreter Interpreter { get; }
        
        public Sandbox(SxlInterpreter interpreter)
        {
            Interpreter = interpreter;
        }

        public bool ProcessPermissionRequest(PermissionRequest request)
        {
            return true;
        }
    }
}