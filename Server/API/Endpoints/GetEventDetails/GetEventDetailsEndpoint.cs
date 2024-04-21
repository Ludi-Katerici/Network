using Contracts.Endpoints.GetEventDetails;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.GetEventDetails;

public class GetEventDetailsEndpoint : Endpoint<GetEventDetailsRequest, GetEventDetailsResponse>
{
    public DataContext DataContext { get; set; }

    public override void Configure()
    {
        this.Get(GetEventDetailsRequest.Route);
    }

    public override async Task HandleAsync(GetEventDetailsRequest req, CancellationToken ct)
    {
        var eventDetails = await this.DataContext.Events
            .Where(x => x.Id == req.Id)
            .Select(x => new GetEventDetailsResponse
            {
                Id = x.Id,
                ImageUrl = x.ProfilePicture,
                Title = x.Title,
                Categories = x.Categories,
                Description = x.Description,
                Region = x.Region,
                Address = x.Address,
                StartDate = x.StartDate,
                Attendees = x.Attendees.Select(attendee => new GetEventDetailsResponse.Attendee
                {
                    Id = attendee.IdentityUser.Id,
                    FullName = $"{attendee.IdentityUser.Name} {attendee.IdentityUser.Family}",
                    Interests = attendee.IdentityUser.Interests,
                    Searchings = attendee.IdentityUser.Searchings,
                    Work = attendee.IdentityUser.Work
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (eventDetails is null)
        {
            await this.SendErrorsAsync();
        }
        else
        {
            await this.SendAsync(eventDetails);
        }
    }
}