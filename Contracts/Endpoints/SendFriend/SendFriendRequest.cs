namespace Contracts.Endpoints.SendFriend;

public class SendFriendRequest 
{
    public const string Route = "/send-friend-request";
    
    public required Guid ReceiverId { get; set; }
}