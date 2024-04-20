using Contracts.Endpoints.GetAllEvents;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.GetAllEvents;

public class GetAllEventsEndpoint : Endpoint<GetAllEventsRequest, GetAllEventsResponseModel>
{
    public DataContext DataContext { get; set; }

    public override void Configure()
    {
        this.Get(GetAllEventsRequest.Route);
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllEventsRequest req, CancellationToken ct)
    {
        var events = await this.DataContext.Events.Select(x => new GetAllEventsResponseModel.EventModel
        {
            Id = x.Id,
            ImageUrl = "https://encrypted-tbn2.gstatic.com/licensed-image?q=tbn:ANd9GcSHcaIGtAL8YQy1NEqScVdwj0ILcyFwC1mQqT6_v3Q_q_XGO-W3xVj92r-BbcEhDSoM8f3a3mfik4zEjdc",
            Title = x.Title,
            Region = x.Region,
            City = x.City,
            Categories = x.Categories,
            Description = x.Description,
            CreatorFullName = $"{x.Creator.Name} {x.Creator.Family}",
        }).ToListAsync();

        await this.SendAsync(
            new GetAllEventsResponseModel
            {
                Events = events
            });
    }
}