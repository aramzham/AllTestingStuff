namespace Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source) =>
        source ?? Enumerable.Empty<TSource>();

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this) => @this?.Any() ?? false;
}