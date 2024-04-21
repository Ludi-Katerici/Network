using Contracts.Endpoints.GetChatMessages;
using Contracts.Endpoints.GetChats;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Persistence;

namespace Server.API.Endpoints.GetChatMessages;

public class GetChatMessagesRequest : Endpoint<GetChatsRequest, GetChatMessagesResponse>
{
    public DataContext Context { get; set; }
    
    public override void Configure()
    {
        this.Post("/friends/get-chat-messages");
    }

    public override async Task HandleAsync(GetChatsRequest req, CancellationToken ct)
    {
        var userId = this.User.GetUserId();
        
        var friend = await this.Context.FriendRelationships
            .Where(x => (x.SenderId == userId || x.ReceiverId == userId) && (x.SenderId == req.FriendId || x.ReceiverId == req.FriendId))
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();

        if (friend == null)
        {
            await this.SendErrorsAsync();
            return;
        }
        
        
        var messages = friend.Messages.Select(x => new GetChatMessagesResponse.ChatMessage
        {
            Id = x.SenderId,
            CreatedAt = x.CreatedOn.DateTime,
            Content = x.Content,
            SenderName = x.SenderName,
            ProfilePicture = x.ProfilePicture
        }).ToList();
        
        await this.SendAsync(new GetChatMessagesResponse
        {
            Messages = messages
        });
    }
}