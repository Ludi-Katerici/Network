using Server.Common.Core;
using Server.Common.Core.Abstract.Auth;
using Server.Common.Core.Models;

namespace Server.Core.Models;

public sealed class AuthorizationDetails : ValueObject
{
    public List<ClaimModel> Claims { get; private set; } = [];
    public List<GenericRoleModel> Roles { get; private set; } = [];
    public List<GenericPermissionModel> Permissions { get; private set; } = [];

    public void AddClaim(IClaim claim)
    {
        this.ReinitializeProperties();
        
        if (this.Claims.Any(x => x.Type == claim.Type))
        {
            throw new Exception($"Claim with type {claim.Type} already exists");
        }

        this.Claims.Add(new ClaimModel(claim.Type, claim.Value));
    }
    
    public void AddRole(IRole role)
    {
        this.ReinitializeProperties();

        this.Roles.Add(new GenericRoleModel(role.Discriminator));
    }
    
    public void AddPermission(IPermission permission)
    {
        this.ReinitializeProperties();
        
        this.Permissions.Add(new GenericPermissionModel(permission.Discriminator));
    }
    
    private void ReinitializeProperties()
    {
        this.Claims ??= [];
        this.Roles ??= [];
        this.Permissions ??= [];
    }
    
    public AuthorizationDetails() {}
}