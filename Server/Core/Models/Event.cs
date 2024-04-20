using Server.Common.Core;

namespace Server.Core.Models;

public sealed class Event : Entity<Guid>, IAuditInformation
{
    public Event(string profilePicture, string title, string region, string city, string address, List<string> categories, string description, Guid creatorId, DateTime startDate) 
        : base(Guid.NewGuid())
    {
        this.ProfilePicture = profilePicture;
        this.Title = title;
        this.Region = region;
        this.City = city;
        this.Address = address;
        this.Categories = categories;
        this.Description = description;
        this.CreatorId = creatorId;
        this.StartDate = startDate;
    }
    
    public string ProfilePicture { get; set; }
    public string Title { get; set; }
    
    public string Region { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public List<string> Categories { get; set; }
    public string Description { get; set; }
    
    public Guid CreatorId { get; set; }
    public IdentityUser Creator { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public List<EventIdentityUser> Attendees { get; set; } = new();
    
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}