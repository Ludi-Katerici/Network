using Server.Common.Core;

namespace Server.Core.Models;

public sealed class LoginInfo : ValueObject
{
    public LoginInfo(
        DateTimeOffset occurredOn,
        string ipAddress,
        string country,
        List<Permission> permissions)
    {
        if (permissions.Count is 0) 
            throw new ArgumentException("Permissions cannot be empty", nameof(permissions));
        
        this.OccurredOn = occurredOn;
        this.IpAddress = ipAddress;
        this.Country = country;
        this.Permissions = permissions;
    }
    
    public DateTimeOffset OccurredOn { get; private set; }
    
    public string IpAddress { get; private set; }
    
    public string Country { get; private set; }
    
    public List<Permission> Permissions { get; private set; }
    
    private LoginInfo() {}
}

