namespace Contracts.Endpoints.GetChats;

public class GetChatsResponse
{
    public required List<ChatModel> Chats { get; set; } = [];
    
    public class ChatModel
    {
        public required Guid FriendId { get; set; }
        public required string FullName { get; set; }
        public required string ProfilePictureUrl { get; set; }
        
        public required DateTime? LastMessageDate { get; set; }
        public required string? LastMessageContent { get; set; }
    }
}