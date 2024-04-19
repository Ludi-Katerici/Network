namespace Server.Modules.Identity.API.SDK.Endpoints.LoginUser;

public sealed record LoginUserRequest
{
    public const string Route = "/identity/login-user";
    
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}