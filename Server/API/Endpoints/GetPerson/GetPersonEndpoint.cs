using Contracts.Endpoints.GetPeople;
using Contracts.Endpoints.GetPerson;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.GetPerson;

public class GetPersonEndpoint : Endpoint<Contracts.Endpoints.GetPerson.GetPerson, GetPersonResponse>
{
    public DataContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Post(Contracts.Endpoints.GetPerson.GetPerson.Route);
    }

    public override async Task HandleAsync(Contracts.Endpoints.GetPerson.GetPerson req, CancellationToken ct)
    {
        var personEntity = await this.DataContext.Users
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
                AttendedEvents = x.EventsAttended.Select(y => new GetPeopleResponse.AttendedEvent
                {
                    Id = y.Event.Id,
                    Name = y.Event.Title,
                    StartDate = y.Event.StartDate,
                    PictureUrl = y.Event.ProfilePicture
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == req.Id);
        
        if (personEntity is null)
        {
            await this.SendNotFoundAsync();
            return;
        }

        var person = new GetPeopleResponse.Person
        {
            Id = personEntity.Id,
            ProfilePictureUrl = personEntity.ProfilePictureUrl,
            Name = personEntity.Name,
            Family = personEntity.Family,
            Email = personEntity.Email,
            Region = personEntity.Region,
            Education = personEntity.Education,
            Work = personEntity.Work,
            ProfessionalExperience = personEntity.ProfessionalExperience,
            Interests = personEntity.Interests.Split(",").ToList(),
            Searchings = personEntity.Searchings.Split(",").ToList(),
            AdditionalInformation = personEntity.AdditionalInformation,
            FriendsCount = personEntity.FriendsCount,
            AttendedEvents = personEntity.AttendedEvents
        };
        


        await this.SendOkAsync(new GetPersonResponse
        {
            Person = person,
        });
    }
}
