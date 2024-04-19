namespace Server.Common.Core;

public sealed class Step<TValue>
{
    public TValue Value { get; private set; }

    public void SetValue(TValue value)
    {
        if (value is null || value.Equals(default))
        {
            return;
        }
        
        this.Value = value;
        this.HasValue = true;
    }
    
    public bool HasValue { get; private set; }
}

public sealed class NullableStep<TValue>
{
    public TValue? Value { get; private set; }

    public void SetValue(TValue? value)
    {
        this.Value = value;
        this.HasValue = true;
    }
    
    public bool HasValue { get; private set; }
}