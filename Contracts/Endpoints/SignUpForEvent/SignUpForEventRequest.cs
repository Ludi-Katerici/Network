namespace Contracts.Endpoints.SignUpForEvent;

public class SignUpForEventRequest
{
    public const string Route = "/events/sign-up";
    
    public required Guid EventId { get; set; }
}