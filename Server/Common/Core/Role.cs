namespace Server.Common.Core;

public sealed class Role : Enumeration
{
    public static readonly Role LowAdmin = new(1, nameof(LowAdmin));
    public static readonly Role MidAdmin = new(2, nameof(MidAdmin));
    public static readonly Role HighAdmin = new(3, nameof(HighAdmin));

    private Role(int value)
        : this(value: value, name: FromValue<Role>(value).Name)
    {
    }

    private Role(int value, string name)
        : base(value: value, name: name)
    {
    }
}