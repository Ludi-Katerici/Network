namespace FrontEnd.Infrastructure.UtilityMethods;

static internal class PrimitiveExtensions
{
    public static bool IsNullOrWhiteSpace(this IEnumerable<string> values) 
        => values.All(string.IsNullOrWhiteSpace);
}