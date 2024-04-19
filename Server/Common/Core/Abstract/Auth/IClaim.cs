namespace Server.Common.Core.Abstract.Auth;

public interface IClaim
{
    string Type { get; }
    
    string Value { get; }
}