using Blazored.LocalStorage;

namespace FrontEnd.Infrastructure.Authentication;

public sealed class AuthService : IAuthService
{
    private const string AuthStateDetailsKey = "authStateDetails";
    
    private readonly ILocalStorageService localStorageService;
    private readonly MemoryStorageUtility memoryStorageUtility;
    public AuthService(ILocalStorageService localStorageService, MemoryStorageUtility memoryStorageUtility)
    {
        this.localStorageService = localStorageService;
        this.memoryStorageUtility = memoryStorageUtility;
    }
    
    public async Task<bool> IsAuthenticated()
    {
        if (this.IsAuthDetailsInMemoryStorage())
        {
            return true;
        }
        
        if (await this.IsAuthDetailsInLocalStorage())
        {
            var authStateDetails = await this.GetStateDetailsFromLocalStorage();
            this.SetAuthDetailsToMemoryStorage(authStateDetails);
            return true;
        }
        
        return false;
    }
    
    public async Task<AuthStateDetails?> GetAuthStateDetails()
    {
        if (this.IsAuthDetailsInMemoryStorage())
        {
            var authStateDetails = this.GetStateDetailsFromMemoryStorage();
            
            if (authStateDetails is not null)
            {
                return authStateDetails;
            }
        }
        
        if (await this.IsAuthDetailsInLocalStorage())
        {
            var authStateDetails = await this.GetStateDetailsFromLocalStorage();
            this.SetAuthDetailsToMemoryStorage(authStateDetails);
            return authStateDetails;
        }
        
        return null;
    }
    
    public async Task SetAuthStateDetails(AuthStateDetails authStateDetails)
    {
        await this.SetStateDetailsToLocalStorage(authStateDetails);
        this.SetAuthDetailsToMemoryStorage(authStateDetails);
    }
    
    private void SetAuthDetailsToMemoryStorage(AuthStateDetails authStateDetails) 
        => this.memoryStorageUtility.Storage[AuthStateDetailsKey] = authStateDetails;
    
    private async Task SetStateDetailsToLocalStorage(AuthStateDetails authStateDetails) 
        => await this.localStorageService.SetItemAsync(AuthStateDetailsKey, authStateDetails);
    
    private async Task<AuthStateDetails> GetStateDetailsFromLocalStorage() 
        => await this.localStorageService.GetItemAsync<AuthStateDetails>(AuthStateDetailsKey);
    
    private AuthStateDetails? GetStateDetailsFromMemoryStorage() 
        => this.memoryStorageUtility.Storage[AuthStateDetailsKey] as AuthStateDetails;
    
    private bool IsAuthDetailsInMemoryStorage() 
        => this.memoryStorageUtility.Storage.ContainsKey(AuthStateDetailsKey);
    
    private async Task<bool> IsAuthDetailsInLocalStorage() 
        => await this.localStorageService.ContainKeyAsync(AuthStateDetailsKey);
}