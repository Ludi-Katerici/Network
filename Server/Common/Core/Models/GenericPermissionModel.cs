using Server.Common.Core.Abstract.Auth;

namespace Server.Common.Core.Models;

public sealed class GenericPermissionModel : IPermission
{
    public GenericPermissionModel(string discriminator) 
        => this.Discriminator = discriminator;
    public string Discriminator { get; set; }
    
    private GenericPermissionModel() {}
}