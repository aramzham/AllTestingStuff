namespace Extensions;

public static class ConsecutiveExtensions
{
    public static bool Any<T>(this IEnumerable<T> @this, Func<T, T, bool> f) => @this.GetEnumerator().Any(f);

    public static bool Any<T>(this IEnumerator<T> @this, Func<T, T, bool> f) =>
        @this.MoveNext().Map(x => Any(@this, f, @this.Current));

    public static bool Any<T>(this IEnumerator<T> @this, Func<T, T, bool> f, T prev) =>
        @this.MoveNext() && (f(prev, @this.Current) || Any(@this, f, @this.Current));

    public static bool ContainsConsecutiveNumbers(this IEnumerable<int> arr) => arr.Any((x, y) => y == x + 1);
}