namespace FrontEnd.Infrastructure;

internal sealed class SelectableItem<T>
{
    public SelectableItem(T value) => this.Value = value;
    public T Value { get; init; }

    public bool IsSelected { get; set; } = false;
}