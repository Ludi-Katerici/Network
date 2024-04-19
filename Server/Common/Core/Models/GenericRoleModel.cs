using Server.Common.Core.Abstract.Auth;

namespace Server.Common.Core.Models;

public sealed class GenericRoleModel : IRole 
{
    public GenericRoleModel(string discriminator) 
        => this.Discriminator = discriminator;
    public string Discriminator { get; set; }
    
    private GenericRoleModel() {}
}