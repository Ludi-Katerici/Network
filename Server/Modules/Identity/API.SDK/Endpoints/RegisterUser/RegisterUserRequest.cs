namespace Server.Modules.Identity.API.SDK.Endpoints.RegisterUser;

public sealed record RegisterUserRequest
{
    public const string Route = "/identity/register-user";
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
}