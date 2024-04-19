using Server.Common.Core.Abstract.Auth;

namespace Server.Common.API.Authorization.Claims;

public sealed class OwnerClaim : IClaim
{
    public OwnerClaim(string companyId) 
        => this.Value = companyId;
    public string Type => "Owner";
    public string Value { get; } 
}