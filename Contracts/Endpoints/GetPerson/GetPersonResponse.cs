using Contracts.Endpoints.GetPeople;

namespace Contracts.Endpoints.GetPerson;

public sealed class GetPersonResponse
{
    public required GetPeopleResponse.Person Person { get; init; }
}