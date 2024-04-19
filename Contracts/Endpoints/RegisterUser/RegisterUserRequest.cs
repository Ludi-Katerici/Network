namespace Contracts.Endpoints.RegisterUser;

public sealed record RegisterUserRequest
{
    public const string Route = "/identity/register-user";
    
    public required string Name { get; set; }
    public required string Family { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required string Region { get; set; }
    public required string City { get; set; }
    public required string ProfessionalExperience { get; set; }
    public required string Interests { get; set; }
    public required string Searchings { get; set; }
    public required string AdditionalInformation { get; set; }
}