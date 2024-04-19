using Server.Common.Core.Abstract.Auth;

namespace Server.Common.API.Authorization.Roles.System;

public sealed class OwnerSystemRole : IRole
{
    public string Discriminator => DiscriminatorValue;
    
    public const string DiscriminatorValue = "Owner";
}