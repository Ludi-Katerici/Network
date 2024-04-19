namespace Server.Common.Core.Abstract;

public interface IClock
{
    DateTimeOffset CurrentDateTime();
    DateOnly CurrentDate();
    TimeOnly CurrentTime();
}