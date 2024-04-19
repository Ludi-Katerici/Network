using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Password;

public sealed class PasswordRequiresLower : IException
{
    public string Discriminator => nameof(PasswordRequiresLower);
    
    public static PasswordRequiresLower Instance { get; } = new();
}
