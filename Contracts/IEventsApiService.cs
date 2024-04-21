using Contracts.Endpoints.CreateEvent;
using Contracts.Endpoints.GetAllEvents;
using Contracts.Endpoints.GetCategories;
using Contracts.Endpoints.GetEventDetails;
using Contracts.Endpoints.GetPeople;
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
    
    [Get(Endpoints.GetCategories.GetCategories.Route)]
    Task<ApiResponse<GetCategoriesResponse>> GetCategories();
    
    [Get(GetPeople.Route)]
    Task<ApiResponse<GetPeopleResponse>> GetAllPeople();
}