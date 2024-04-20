namespace FrontEnd.Infrastructure.Authentication;

public sealed class AuthStateDetails
{
    public List<string> Roles { get; set; } = [];
    public List<string> Permissions { get; set; } = [];
    public List<string> Claims { get; set; } = [];

    public string UserId { get; set; } = string.Empty;
}