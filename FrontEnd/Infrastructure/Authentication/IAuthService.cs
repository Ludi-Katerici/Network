namespace FrontEnd.Infrastructure.Authentication;

public interface IAuthService
{
    Task<bool> IsAuthenticated();
    
    Task<AuthStateDetails?> GetAuthStateDetails();
    
    Task SetAuthStateDetails(AuthStateDetails authStateDetails);
}