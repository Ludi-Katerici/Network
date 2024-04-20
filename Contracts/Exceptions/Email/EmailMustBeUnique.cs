namespace Contracts.Exceptions.Email;

public sealed class EmailMustBeUnique : IException
{
    public static EmailMustBeUnique Instance { get; } = new();
    public string ErrorMessage => "Този имейл вече съществува.";
}