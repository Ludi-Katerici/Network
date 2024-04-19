namespace Server.Modules.Identity.Core.Services;

public interface ILocalizatorService 
{
    Task<string> GetCountryFromIp(string ip, CancellationToken cancellationToken = default);
}