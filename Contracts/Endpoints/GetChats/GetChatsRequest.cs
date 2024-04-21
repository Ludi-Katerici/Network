namespace Contracts.Endpoints.GetChats;

public class GetChatsRequest
{
    public const string Route = "/friends/get-chats";
    
    public required Guid FriendId { get; set; }
}

