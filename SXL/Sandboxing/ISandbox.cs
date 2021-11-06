using SXL.Sandboxing.Permissions;

namespace SXL.Sandboxing
{
    public interface ISandbox
    {
        public bool ProcessPermissionRequest(PermissionRequest request);
    }
}