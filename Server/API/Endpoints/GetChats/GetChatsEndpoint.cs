using Contracts.Endpoints.GetChats;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Persistence;

namespace Server.API.Endpoints.GetChats;

public class GetChatsEndpoint : Endpoint<GetChatsRequest, GetChatsResponse>
{
    public DataContext Context { get; set; }

    public override void Configure()
    {
        this.Get(GetChatsRequest.Route);
    }

    public override async Task HandleAsync(GetChatsRequest req, CancellationToken ct)
    {
        var userId = this.User.GetUserId();
        var chats = (await this.Context.FriendRelationships
                .AsNoTracking()
                .Where(x => x.SenderId == this.User.GetUserId() || x.ReceiverId == this.User.GetUserId())
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Messages)
                .Select(x => new
                {
                    FriendId = x.SenderId == userId ? x.ReceiverId : x.SenderId,
                    FullName = x.SenderId == userId ? x.Receiver.Name + " " + x.Receiver.Family : x.Sender.Name + " " + x.Sender.Family,
                    ProfilePictureUrl = x.SenderId == userId ? x.Receiver.ProfilePictureUrl : x.Sender.ProfilePictureUrl,
                    LastMessageDate = x.Messages,
                    LastMessageContent = x.Messages,
                })
                .ToListAsync(ct))
            .Select(x => {
                string? lastMessageContent = null;
                DateTime? lastMessageDate = null;

                if (x.LastMessageContent.Count != 0)
                {
                    lastMessageContent = x.LastMessageContent.Last().Content;
                    lastMessageDate = x.LastMessageContent.Last().CreatedOn.DateTime;
                }

                return new GetChatsResponse.ChatModel
                {
                    FriendId = x.FriendId,
                    FullName = x.FullName,
                    ProfilePictureUrl = x.ProfilePictureUrl,
                    LastMessageDate = lastMessageDate,
                    LastMessageContent = lastMessageContent
                };
            }).ToList();

        await this.SendAsync(new GetChatsResponse { Chats = chats });
    }
}