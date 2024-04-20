namespace Contracts.Endpoints.GetEventDetails;

public class GetEventDetailsRequest
{
    public const string Route = $"/events/get-details/{{{nameof(Id)}}}";
    
    public required Guid Id { get; set; }
}