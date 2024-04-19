using FrontEnd.Infrastructure.UtilityMethods;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Infrastructure.Authentication;

public sealed class AuthorizeComponent : ComponentBase
{
    [Parameter]
    public string[] Roles { get; set; } = Array.Empty<string>();
    
    [Parameter]
    public string[] Permissions { get; set; } = Array.Empty<string>();
    
    [Parameter]
    public string[] Claims { get; set; } = Array.Empty<string>();

    [Parameter, EditorRequired]
    public IAuthService AuthService { get; set; } = default!;
    
    [Parameter, EditorRequired]
    public NavigationManager NavigationManager { get; set; } = default!;

    private const string DefaultRedirectUrl = "/Login";

    protected override async Task OnParametersSetAsync()
    {
        if (this.Roles.IsNullOrWhiteSpace() &&
            this.Permissions.IsNullOrWhiteSpace() &&
            this.Claims.IsNullOrWhiteSpace())
        {
            var isAuthenticated = await this.AuthService.IsAuthenticated();
            
            if (isAuthenticated is false)
            {
                this.NavigationManager.NavigateTo(DefaultRedirectUrl);
            }
            
            return;
        }

        var meetsRequirements = await this.MustHaveAuthorizationRequirements();
        
        if (meetsRequirements is false)
        {
            this.NavigationManager.NavigateTo(DefaultRedirectUrl);
        }
    }
    private async Task<bool> MustHaveAuthorizationRequirements()
    {
        var authState = await this.AuthService.GetAuthStateDetails();

        if (authState is null)
        {
            return false;
        }
        
        return this.Roles.All(role => authState.Roles.Contains(role)) &&
               this.Permissions.All(permission => authState.Permissions.Contains(permission)) &&
               this.Claims.All(claim => authState.Claims.Contains(claim));
    }
}