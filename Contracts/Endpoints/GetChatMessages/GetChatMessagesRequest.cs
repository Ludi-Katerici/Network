﻿namespace Contracts.Endpoints.GetChatMessages;

public sealed class GetChatMessagesRequest
{
    public const string Route = "/friends/get-chat-messages";
    
    public required Guid FriendId { get; set; }
}