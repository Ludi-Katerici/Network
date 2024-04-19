namespace Server.Modules.Identity.API.SDK.Endpoints.LoginUser;

public sealed record LoginUserResponse
{
    public List<string> Roles { get; init; } = [];
    
    public List<string> Permissions { get; init; } = [];
    
    public List<string> Claims { get; init; } = [];
    
    public Guid UserId { get; init; }
}