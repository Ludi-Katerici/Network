using Contracts.Endpoints.CreateEvent;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.API.Authorization;
using Server.Core.Models;
using Server.Persistence;

namespace Server.API.Endpoints.CreateEvent;

public class CreateEventEndpoint : Endpoint<CreateEventRequest>
{
    public DataContext DataContext { get; set; }

    public override void Configure()
    {
        this.Post(CreateEventRequest.Route);
    }

    public override async Task HandleAsync(CreateEventRequest req, CancellationToken ct)
    {
        var userId = this.User.GetUserId();
        var user = await this.GetIdentityUser(userId, ct);
        
        if (user is null)
        {
            await this.SendNotFoundAsync();
            return;
        }
        
        var @event = new Event(
            profilePicture: req.PictureUrl,
            title: req.Name,
            region: req.Region,
            address: req.Address,
            categories: req.Categories,
            description: req.Description,
            creatorId: user.Id,
            startDate: req.StartDate);
        
        await this.DataContext.Events.AddAsync(@event, ct);
        var rows = await this.DataContext.SaveChangesAsync(ct);
        if (rows == 0)
        {
            await this.SendErrorsAsync();
            return;
        }

        await this.SendOkAsync();
    }
    
    private async Task<IdentityUser?> GetIdentityUser(Guid id, CancellationToken ct)
    {
        return await this.DataContext.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}