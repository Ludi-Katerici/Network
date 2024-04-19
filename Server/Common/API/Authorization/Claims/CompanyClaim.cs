using Server.Common.Core.Abstract.Auth;

namespace Server.Common.API.Authorization.Claims;

public sealed class CompanyClaim : IClaim
{
    public CompanyClaim(string companyId, string companyName) 
        => this.Value = $"{companyId}{Separator}{companyName}";

    public string Type => TypeValue;
    public string Value { get; }

    public const string TypeValue = "Company";
    
    private const string Separator = ":";
    
    public (string companyId, string companyName) GetValues()
    {
        var values = this.Value.Split(Separator);
        return (values[0], values[1]);
    }
    
    public static (string CompanyId, string CompanyName) GetValues(string value)
    {
        var values = value.Split(Separator);
        return (values[0], values[1]);
    }
}