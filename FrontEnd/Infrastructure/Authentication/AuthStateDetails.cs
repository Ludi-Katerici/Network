namespace FrontEnd.Infrastructure.Authentication;

public sealed class AuthStateDetails
{
    public List<string> Roles { get; set; } = [];
    public List<string> Permissions { get; set; } = [];
    public List<string> Claims { get; set; } = [];

    public string UserId { get; set; } = string.Empty;
    
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    
    public List<string> Sites { get; set; } = [];
}