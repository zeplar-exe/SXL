using SXL.Interpreter;
using SXL.Sandboxing.Permissions;

namespace SXL.Sandboxing
{
    public interface ISandbox
    {
        public SxlInterpreter Interpreter { get; }
        
        public bool ProcessPermissionRequest(PermissionRequest request);
    }
}