using Server.Common.Core;

namespace Server.Core.Models;

public sealed class Event : Entity<Guid>, IAuditInformation
{
    public string Title { get; set; }
    
    public string Region { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public List<string> Categories { get; set; }
    public string Description { get; set; }
    
    public Guid CreatorId { get; set; }
    public IdentityUser Creator { get; set; }
    
    public DateTimeOffset StartDate { get; set; }
    
    public List<EventIdentityUser> Attendees { get; set; } = new();
    
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}