namespace Contracts.Endpoints.SendMessage;

public sealed class SendMessageRequest
{
    public const string Route = "/friends/send-message";
    
    public required Guid FriendId { get; set; }
    
    public required string Content { get; set; }
}