namespace Server.API.SDK.Endpoints.RegisterUser;

public sealed record RegisterUserResponse
{
    public required Guid Id { get; set; }
    
    public required string Email { get; set; }
}