using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.API.SDK.Endpoints.IsEmailAvailable;
using Server.Persistence;

namespace Server.API.Endpoints.IsEmailAvailable;

internal sealed class IsEmailAvailableEndpoint : Endpoint<IsEmailAvailableRequest, bool>
{
    public DataContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Get(IsEmailAvailableRequest.Route);
        this.AllowAnonymous();
        this.DontThrowIfValidationFails();
    }
    
    public override async Task HandleAsync(IsEmailAvailableRequest req, CancellationToken ct)
    {
        this.Logger.LogInformation("Checking if email {Email} is available", req.Email);
        var isEmailAvailable = await this.IsEmailAvailable(req.Email, ct);

        await this.SendOkAsync(isEmailAvailable);
    }

    public override async Task OnValidationFailedAsync(CancellationToken ct) 
        => await this.SendOkAsync(false);
    
    private async Task<bool> IsEmailAvailable(string email, CancellationToken ct)
    {
        var upperEmail = email.ToUpper();
        var result = await this.DataContext.Users.AsNoTracking().AnyAsync(x => x.Email == upperEmail, ct);
        return result is false;
    }
}