namespace Contracts.Endpoints.AcceptFriendRequest;

public sealed class AcceptFriendRequest
{
    public const string Route = "/accept-friend-request";
    
    public required Guid SenderId { get; set; }
}