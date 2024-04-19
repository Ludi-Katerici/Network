using Auth_IPermission=Server.Common.Core.Abstract.Auth.IPermission;
using IPermission=Server.Common.Core.Abstract.Auth.IPermission;

namespace Server.Common.API.Authorization.Permissions;

public sealed class AccessToSystemPermission : Auth_IPermission
{
    private AccessToSystemPermission() { }
    
    public string Discriminator => "AccessToSystem";
    
    public static AccessToSystemPermission Instance { get; } = new();
}