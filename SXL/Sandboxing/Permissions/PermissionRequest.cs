namespace SXL.Sandboxing.Permissions
{
    public readonly struct PermissionRequest
    {
        public readonly Permission Permission;
        public readonly IPermissionInformation PermissionInformation;

        public PermissionRequest(Permission permission, IPermissionInformation permissionInformation)
        {
            Permission = permission;
            PermissionInformation = permissionInformation;
        }
    }
}