namespace Server.Modules.Identity.API.SDK.Events;

internal sealed class UserCreated
{
    public required Guid Id { get; set; }
}