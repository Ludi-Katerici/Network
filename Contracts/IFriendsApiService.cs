using Contracts.Endpoints.GetAllFriendRequests;
using Contracts.Endpoints.SendFriend;
using Refit;

namespace Contracts;

public interface IFriendsApiService
{
    [Get(GetAllFriendsRequestsRequest.Route)]
    Task<ApiResponse<GetAllFriendsRequestsResponse>> GetAllFriendsRequests();
    
    [Post(SendFriendRequest.Route)]
    Task<IApiResponse> SendFriendRequestTo([Body] SendFriendRequest request);
}