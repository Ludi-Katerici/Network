using Contracts.Endpoints.GetAllFriendRequests;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Persistence;

namespace Server.API.Endpoints.GetAllFriendsRequests;

public class GetAllFriendsRequestsEndpoint : Endpoint<EmptyRequest, GetAllFriendsRequestsResponse>
{
    public DataContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Get(GetAllFriendsRequestsRequest.Route);
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        var userId = this.HttpContext.User.GetUserId();
        var friendRequestsModels = await this.DataContext.FriendRelationships
            .Where(x => x.ReceiverId == userId || x.SenderId == userId)
            .Select(x => new GetAllFriendsRequestsResponse.FriendRequestResponseModel
            {
                SenderId = x.SenderId,
                SenderFullName = x.Sender.Name + " " + x.Sender.Family,
                ReceiverId = x.ReceiverId,
                ReceiverFullName = x.Receiver.Name + " " + x.Receiver.Family,
                CreatedAt = x.CreatedOn.DateTime,
                AcceptedAt = x.AcceptedAt
            })
            .ToListAsync(ct);
        
        var response = new GetAllFriendsRequestsResponse
        {
            FriendRequests = friendRequestsModels
        };

        await this.SendOkAsync(response);
    }
}