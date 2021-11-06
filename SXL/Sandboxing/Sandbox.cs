using SXL.Sandboxing.Permissions;

namespace SXL.Sandboxing
{
    public sealed class Sandbox : ISandbox
    {
        public bool ProcessPermissionRequest(PermissionRequest request)
        {
            return true;
        }
    }
}