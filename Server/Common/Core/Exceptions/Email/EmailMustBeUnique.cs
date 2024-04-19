using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Email;

public sealed class EmailMustBeUnique : IException
{
    public static EmailMustBeUnique Instance { get; } = new();
    public string ErrorMessage => "Този имейл вече съществува.";
}