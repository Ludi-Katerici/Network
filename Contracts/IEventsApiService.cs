using Contracts.Endpoints.CreateEvent;
using Contracts.Endpoints.RegisterUser;
using Refit;

namespace Contracts;

public interface IEventsApiService
{
    [Post(CreateEventRequest.Route)]
    Task<IApiResponse> CreateEvent([Body] CreateEventRequest request);
}