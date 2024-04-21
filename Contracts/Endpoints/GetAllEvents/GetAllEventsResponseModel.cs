namespace Contracts.Endpoints.GetAllEvents;

public class GetAllEventsResponseModel
{
    public required List<EventModel> Events { get; set; }
    
    public class EventModel
    {
        public required Guid Id { get; set; }
        public required string ImageUrl { get; set; }
        public required string Title { get; set; }
        public required string Region { get; set; }
        public required List<string> Categories { get; set; }
        public required string Description { get; set; }
        public required string CreatorFullName { get; set; }
        public required DateTime StartDate { get; set; }
    }
}