using Server.Common.Core.Abstract.Auth;

namespace Server.Common.API.Authorization.Roles.System;

public sealed class SuperAdminSystemRole : IRole
{
    public string Discriminator => DiscriminatorValue;
    
    public const string DiscriminatorValue = "SuperAdmin";
}