namespace Contracts.Endpoints.GetEventDetails;

public class GetEventDetailsResponse
{
    public required Guid Id { get; set; }
    public required string ImageUrl { get; set; }
    public required string Title { get; set; }
    public required List<string> Categories { get; set; }
    public required string Description { get; set; }
    public required string Region { get; set; }
    public required string City { get; set; }
    public required string Address { get; set; }
    public required DateTime StartDate { get; set; }
    
    public List<Attendee> Attendees { get; set; } = [];
    
    public class Attendee
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Interests { get; set; }
        public required string Searchings { get; set; }
        public required string Work { get; set; }
    }
}