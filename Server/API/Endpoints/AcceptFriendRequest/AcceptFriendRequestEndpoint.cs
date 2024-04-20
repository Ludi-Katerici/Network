using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Common.Core.Abstract;
using Server.Persistence;

namespace Server.API.Endpoints.AcceptFriendRequest;

internal sealed class AcceptFriendRequestEndpoint : Endpoint<Contracts.Endpoints.AcceptFriendRequest.AcceptFriendRequest>
{
    public DataContext DataContext { get; set; }
    public IClock Clock { get; set; }
    
    public override void Configure()
    {
        this.Post(Contracts.Endpoints.AcceptFriendRequest.AcceptFriendRequest.Route);
    }

    public override async Task HandleAsync(Contracts.Endpoints.AcceptFriendRequest.AcceptFriendRequest req, CancellationToken ct)
    {
        var receiverId = this.HttpContext.User.GetUserId();
        var friendRequest = await this.DataContext.FriendRelationships.Where(x => x.ReceiverId == receiverId && x.SenderId == req.SenderId).FirstOrDefaultAsync(ct);
        
        if (friendRequest is null)
        {
            await this.SendNotFoundAsync();
            return;
        }

        if (friendRequest.AcceptedAt.HasValue)
        {
            await this.SendOkAsync();
            return;
        }
        
        friendRequest.AcceptedAt = this.Clock.CurrentDateTime().DateTime;
        await this.DataContext.SaveChangesAsync(ct);
        await this.SendOkAsync();
    }
}