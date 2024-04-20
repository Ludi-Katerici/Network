namespace Contracts.Endpoints.CreateEvent;

public sealed class CreateEventRequest
{
    public const string Route = "/events/create";
    
    public required string PictureUrl { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required string Region { get; set; }
    public required string City { get; set; }
    public required string Address { get; set; }
    public required List<string> Categories { get; set; }
}