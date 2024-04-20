using Server.Common.Core;

namespace Server.Core.Models;

public sealed class EventIdentityUser : IAuditInformation
{
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    
    public Guid IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
    
    public bool IsConfirmed { get; set; }
    
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}