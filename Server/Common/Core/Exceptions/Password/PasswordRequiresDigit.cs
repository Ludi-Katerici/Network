using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Password;

public sealed class PasswordRequiresDigit : IException
{
    public string Discriminator => nameof(PasswordRequiresDigit);
    
    public static PasswordRequiresDigit Instance { get; } = new();
}