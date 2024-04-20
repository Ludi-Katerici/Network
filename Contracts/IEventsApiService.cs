using Contracts.Endpoints.CreateEvent;
using Contracts.Endpoints.GetAllEvents;
using Contracts.Endpoints.GetEventDetails;
using Contracts.Endpoints.SendFriend;
using Contracts.Endpoints.SignUpForEvent;
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
    
    [Post(SignUpForEventRequest.Route)]
    Task<IApiResponse> SignUpForEvent([Body] SignUpForEventRequest request);
    
    [Post(SendFriendRequest.Route)]
    Task<IApiResponse> SendFriendRequestTo([Body] SendFriendRequest request);
}