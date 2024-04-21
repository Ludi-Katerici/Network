using Contracts.Endpoints.SendMessage;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Common.Core.Abstract;
using Server.Core.Models;
using Server.Persistence;

namespace Server.API.Endpoints.SendMessage;

public class SendMessageEndpoint : Endpoint<SendMessageRequest>
{
    public DataContext Context { get; set; }
    public IClock Clock { get; set; }
    
    public override void Configure()
    {
        this.Post(SendMessageRequest.Route);
    }

    public override async Task HandleAsync(SendMessageRequest req, CancellationToken ct)
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

        var profilePicture = friend.SenderId == userId ? friend.Sender.ProfilePictureUrl : friend.Receiver.ProfilePictureUrl;
        var senderName = friend.SenderId == userId ? friend.Sender.Name + " " + friend.Sender.Family : friend.Receiver.Name + " " + friend.Receiver.Family;
        
        var message = new Message
        {
            Content = req.Content,
            SenderName = senderName,
            ProfilePicture = profilePicture,
            CreatedOn = this.Clock.CurrentDateTime().DateTime
        };
        
        friend.Messages.Add(message);
        
        await this.Context.SaveChangesAsync(ct);
        
        await this.SendOkAsync();
    }
}