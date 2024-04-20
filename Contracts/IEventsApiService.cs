using Contracts.Endpoints.CreateEvent;
using Contracts.Endpoints.GetAllEvents;
using Contracts.Endpoints.GetEventDetails;
using Refit;

namespace Contracts;

public interface IEventsApiService
{
    [Post(CreateEventRequest.Route)]
    Task<IApiResponse> CreateEvent([Body] CreateEventRequest request);
    
    [Get(GetAllEventsRequest.Route)]
    Task<ApiResponse<GetAllEventsResponseModel>> GetAllEvents();

    [Get(GetEventDetailsRequest.Route)]
    Task<ApiResponse<GetEventDetailsResponse>> GetEventDetails(Guid id);
}