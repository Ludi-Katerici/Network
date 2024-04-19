namespace Server.API.SDK.Endpoints.DeleteUser;

public sealed record DeleteUserRequest 
{
    public const string Route = $"/identity/delete-user/{{{nameof(UserId)}}}";
    
    public Guid UserId { get; init; }
}