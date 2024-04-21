using Contracts.Endpoints.AcceptFriendRequest;
using Contracts.Endpoints.DeleteFriendRequest;
using Contracts.Endpoints.GetAllFriendRequests;
using Contracts.Endpoints.SendFriend;
using Contracts.Endpoints.SendMessage;
using Refit;

namespace Contracts;

public interface IFriendsApiService
{
    [Get(GetAllFriendsRequestsRequest.Route)]
    Task<ApiResponse<GetAllFriendsRequestsResponse>> GetAllFriendsRequests();
    
    [Post(SendFriendRequest.Route)]
    Task<IApiResponse> SendFriendRequestTo([Body] SendFriendRequest request);
    
    [Post(AcceptFriendRequest.Route)]
    Task<IApiResponse> AcceptAFriendRequest([Body] AcceptFriendRequest request);
    
    [Post(DeleteFriendRequest.Route)]
    Task<IApiResponse> DeleteAFriendRequest([Body] DeleteFriendRequest request);
    
    [Post(SendMessageRequest.Route)]
    Task<IApiResponse> SendMessageToFriend([Body] SendMessageRequest request);
}