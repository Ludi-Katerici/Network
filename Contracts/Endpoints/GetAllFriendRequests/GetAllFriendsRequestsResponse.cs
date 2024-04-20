namespace Contracts.Endpoints.GetAllFriendRequests;

public sealed class GetAllFriendsRequestsResponse
{
    public required List<FriendRequestResponseModel> FriendRequests { get; set; } = [];

    public class FriendRequestResponseModel
    {
        public required Guid SenderId { get; set; }
        public required string SenderFullName { get; set; }
        public required Guid ReceiverId { get; set; }
        public required string ReceiverFullName { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime? AcceptedAt { get; set; }
    }
}