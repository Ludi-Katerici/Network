using Contracts.Endpoints.SignUpForEvent;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Core.Models;
using Server.Persistence;

namespace Server.API.Endpoints.SignUpForEvent;

public class SignUpForEvent : Endpoint<SignUpForEventRequest>
{
    public DataContext Context { get; set; }
    
    public override void Configure()
    {
        this.Post(SignUpForEventRequest.Route);
    }

    public override async Task HandleAsync(SignUpForEventRequest req, CancellationToken ct)
    {
        var userId = this.User.GetUserId();
        
        var exists = await this.Context.Events.AnyAsync(x => x.Id == req.EventId);
        
        if (exists is false)
        {
            await this.SendNotFoundAsync();
            return;
        }
        
        if (this.Context.EventIdentityUsers.Any(x => x.EventId == req.EventId && x.IdentityUserId == userId))
        {
            await this.SendOkAsync();
            return;
        }

        this.Context.EventIdentityUsers.Add(new EventIdentityUser
        {
            EventId = req.EventId,
            IdentityUserId = userId
        });
        
        await this.Context.SaveChangesAsync(ct);
        
        await this.SendOkAsync();
    }
}