namespace Server.Common.Core.Models;

public sealed class ClaimModel : ValueObject
{
    public ClaimModel(
        string type,
        string value)
    {
        this.Type = type;
        this.Value = value;
    }
    
    public string Type { get; private set; }
    
    public string Value { get; private set; }
    
    private ClaimModel() {}
}