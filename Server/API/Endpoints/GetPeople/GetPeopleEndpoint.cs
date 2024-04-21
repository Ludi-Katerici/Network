using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.GetPeople;

public class GetPeopleEndpoint : Endpoint<Contracts.Endpoints.GetPeople.GetPeople, Contracts.Endpoints.GetPeople.GetPeopleResponse>
{
    public DataContext DataContext { get; set; }

    public override void Configure()
    {
        this.Get(Contracts.Endpoints.GetPeople.GetPeople.Route);
    }

    public override async Task HandleAsync(Contracts.Endpoints.GetPeople.GetPeople req, CancellationToken ct)
    {
        var people = (await this.DataContext.Users
                .AsNoTracking()
                .Select(x => new
                {
                    Id = x.Id,
                    ProfilePictureUrl = x.ProfilePictureUrl,
                    Name = x.Name,
                    Family = x.Family,
                    Email = x.Email,
                    Region = x.Region,
                    Education = x.Education,
                    Work = x.Work,
                    ProfessionalExperience = x.ProfessionalExperience,
                    Interests = x.Interests,
                    Searchings = x.Searchings,
                    AdditionalInformation = x.AdditionalInformation,
                    FriendsCount = x.FriendRelationships.Count(),
                    AttendedEvents = x.EventsAttended.Select(y => new Contracts.Endpoints.GetPeople.GetPeopleResponse.AttendedEvent
                    {
                        Id = y.Event.Id,
                        Name = y.Event.Title,
                        StartDate = y.Event.StartDate,
                        PictureUrl = y.Event.ProfilePicture
                    }).ToList()
                }).ToListAsync())
            .Select(x => new Contracts.Endpoints.GetPeople.GetPeopleResponse.Person
            {
                Id = x.Id,
                ProfilePictureUrl = x.ProfilePictureUrl,
                Name = x.Name,
                Family = x.Family,
                Email = x.Email,
                Region = x.Region,
                Education = x.Education,
                Work = x.Work,
                ProfessionalExperience = x.ProfessionalExperience,
                Interests = x.Interests.Split(",").ToList(),
                Searchings = x.Searchings.Split(",").ToList(),
                AdditionalInformation = x.AdditionalInformation,
                FriendsCount = x.FriendsCount,
                AttendedEvents = x.AttendedEvents
            }).ToList();


        await this.SendOkAsync(
            new Contracts.Endpoints.GetPeople.GetPeopleResponse
            {
                People = people
            });
    }
}