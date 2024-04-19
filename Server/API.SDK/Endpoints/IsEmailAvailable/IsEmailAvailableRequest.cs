namespace Server.API.SDK.Endpoints.IsEmailAvailable;

public sealed record IsEmailAvailableRequest
{
    public const string Route = $"/identity/is-email-available/{{{nameof(Email)}}}";
    
    public string Email { get; init; }
}