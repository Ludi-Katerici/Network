using Server.Common.Core;

namespace Server.Core.Models;

public sealed class Permission : ValueObject
{
    public Permission(Guid siteId, Role role)
    {
        this.SiteId = siteId;
        this.Role = role;
    }
    
    public Guid SiteId { get; init; }
    public Role Role { get; init; }
    
    private Permission() {}
}