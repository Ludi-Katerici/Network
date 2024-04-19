using Server.Common.Core.Abstract;

namespace Server.Common.Infrastructure;

internal sealed class Clock : IClock
{
    public DateTimeOffset CurrentDateTime() => DateTimeOffset.Now;
    public DateOnly CurrentDate() => DateOnly.FromDateTime(this.CurrentDateTime().DateTime);
    public TimeOnly CurrentTime() => TimeOnly.FromDateTime(this.CurrentDateTime().DateTime);
}