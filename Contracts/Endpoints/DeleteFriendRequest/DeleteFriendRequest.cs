namespace Contracts.Endpoints.DeleteFriendRequest;

public sealed class DeleteFriendRequest
{
    public const string Route = "/delete-friend-request";
    
    public required Guid ReceiverId { get; set; }
    public required Guid SenderId { get; set; }
}