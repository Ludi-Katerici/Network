namespace Contracts.Endpoints.RegisterUser;

public sealed record RegisterUserRequest
{
    public const string Route = "/identity/register-user";
    
    public string ProfilePictureUrl { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Education { get; set; }
    public string Work { get; set; }
    public string ProfessionalExperience { get; set; }
    public string Interests { get; set; }
    public string Searchings { get; set; }
    public string AdditionalInformation { get; set; }
}