namespace Extensions;

public static class StringExtensions
{
    public static int ValueOrDefault(this string @this, int defaultValue) =>
        string.IsNullOrWhiteSpace(@this) || !int.TryParse(@this, out var parsedValue) ? defaultValue : parsedValue;

    public static string ValueOrDefault(this string @this, string defaultValue) =>
        string.IsNullOrWhiteSpace(@this) ? defaultValue : @this;
}