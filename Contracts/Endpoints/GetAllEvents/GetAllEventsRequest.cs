namespace Contracts.Endpoints.GetAllEvents;

public class GetAllEventsRequest
{
    public static GetAllEventsRequest Instance = new();
    
    public const string Route = "/events/get-all-events";
}

