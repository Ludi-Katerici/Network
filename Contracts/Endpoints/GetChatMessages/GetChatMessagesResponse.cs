namespace Contracts.Endpoints.GetChatMessages;

public sealed class GetChatMessagesResponse
{
    public List<ChatMessage> Messages { get; set; } = [];

    public sealed class ChatMessage
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public string ProfilePicture { get; set; }
    }
}