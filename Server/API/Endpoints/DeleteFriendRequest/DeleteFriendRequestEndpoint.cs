using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Persistence;

namespace Server.API.Endpoints.DeleteFriendRequest;

internal sealed class DeleteFriendRequestEndpoint : Endpoint<Contracts.Endpoints.DeleteFriendRequest.DeleteFriendRequest>
{
    public DataContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Post(Contracts.Endpoints.DeleteFriendRequest.DeleteFriendRequest.Route);
    }

    public override async Task HandleAsync(Contracts.Endpoints.DeleteFriendRequest.DeleteFriendRequest req, CancellationToken ct)
    {
        var userId = this.HttpContext.User.GetUserId();
        
        Console.WriteLine($"User ID: {userId}");
        Console.WriteLine($"Sender ID: {req.SenderId}");
        Console.WriteLine($"Receiver ID: {req.ReceiverId}");
        var rows = await this.DataContext.FriendRelationships
            .Where(x => x.SenderId == req.SenderId && x.ReceiverId == req.ReceiverId && (x.ReceiverId == userId || x.SenderId == userId))
            .ExecuteDeleteAsync(ct);

        if (rows == 1)
        {
            await this.SendOkAsync();
            return;
        }
        
        await this.SendNotFoundAsync();
    }
}