namespace Extensions;

public static class FunctionalExtensions
{
    public static TOutput Map<TInput, TOutput>(this TInput @this, Func<TInput, TOutput> f) => f(@this);

    public static T Tee<T>(this T @this, Action<T> act)
    {
        act(@this);
        return @this;
    }

    public static T2 DoIfNotNull<T1, T2>(this T1 @this, Func<T1, T2> f) =>
        !EqualityComparer<T1>.Default.Equals(@this, default) ? f(@this) : default;

    public static bool Validate<T>(this T @this, params Func<T, bool>[] predicates) => predicates.All(x => x(@this));

    public static T ApplyOps<T>(this T @this, params Func<T, T>[] f) => f.Aggregate(@this, (acc, x) => x(acc));

    public static IEnumerable<T>
        Adjust<T>(this IEnumerable<T> @this, Func<T, int, bool> shouldReplace, T replacement) =>
        @this.Select((obj, pos) => shouldReplace(obj, pos) ? replacement : obj);
}