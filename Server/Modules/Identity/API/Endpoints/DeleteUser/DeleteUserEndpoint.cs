﻿using FastEndpoints;
using Server.Modules.Identity.API.SDK.Endpoints.DeleteUser;

namespace Server.Modules.Identity.API.Endpoints.DeleteUser;

internal sealed class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
{
    public IdentityContext DataContext { get; set; }
    
    public override void Configure()
    {
        this.Delete(DeleteUserRequest.Route);
    }

    public override Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        var rows = this.DataContext.Users.Where(x => x.Id == req.UserId).ExecuteDelete();
        
        return rows == 0 ? this.SendNotFoundAsync() : this.SendOkAsync();

    }
}