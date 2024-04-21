namespace Contracts.Endpoints.GetPerson;

public sealed class GetPerson
{
    public const string Route = "/get-person";
    
    public required Guid Id { get; init; }
}