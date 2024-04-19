namespace FrontEnd.Infrastructure;

public sealed class MemoryStorageUtility
{
    public Dictionary<string, object> Storage { get; set; } = new();
}