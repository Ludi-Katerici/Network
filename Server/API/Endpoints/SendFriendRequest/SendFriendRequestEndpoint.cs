using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Core.Models;
using Server.Persistence;

namespace Server.API.Endpoints.SendFriendRequest;

public sealed class SendFriendRequestEndpoint : Endpoint<Contracts.Endpoints.SendFriend.SendFriendRequest>
{
    public DataContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Post(Contracts.Endpoints.SendFriend.SendFriendRequest.Route);
    }

    public override async Task HandleAsync(Contracts.Endpoints.SendFriend.SendFriendRequest req, CancellationToken ct)
    {
        var senderId = this.User.GetUserId();
        
        var hasAlreadyFriendRequest = await this.DataContext.FriendRelationships.AnyAsync(x => (x.SenderId == senderId && x.ReceiverId == req.ReceiverId) ||
                                                                              (x.SenderId == req.ReceiverId && x.ReceiverId == senderId));
        
        if (hasAlreadyFriendRequest)
        {
            await this.SendNotFoundAsync();
            return;
        }

        this.DataContext.FriendRelationships.Add(new FriendRelationship()
        {
            SenderId = senderId,
            ReceiverId = req.ReceiverId
        });
        
        await this.DataContext.SaveChangesAsync(ct);
        
        await this.SendOkAsync();
    }
}