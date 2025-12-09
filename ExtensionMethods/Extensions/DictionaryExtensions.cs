using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Extensions;

public static class DictionaryExtensions
{
    public static IDictionary<TK, TV> ToDictionary<TK, TV>(this IEnumerable<(TK, TV)> @this) =>
        @this.ToDictionary(x => x.Item1, x => x.Item2);

    public static IDictionary<TK, TV> ToDictionary<TK, TV>(this IEnumerable<KeyValuePair<TK, TV>> @this) =>
        @this.ToDictionary(k => k.Key, v => v.Value);

    public static IDictionary<TK, int> ToCountDictionary<TK>(this IEnumerable<TK> @this) =>
        @this.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Count());

    public static Func<TK, TV> ToLookupWithDefault<TK, TV>(this IDictionary<TK, TV> @this, TV defaultValue) =>
        x => @this.ContainsKey(x) ? @this[x] : defaultValue;

    public static Func<TK, TV> ToLookupWithDefault<TK, TV>(this IDictionary<TK, TV> @this) =>
        x => @this.ContainsKey(x) ? @this[x] : default;
    
    public static TValue? GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue? value) where TKey : notnull
    {
        ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out var exists);
        if (exists)
        {
            return val;
        }
        
        val = value;
        return value;
    }
    
    public static bool TryUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value) where TKey : notnull
    {
        ref var val = ref CollectionsMarshal.GetValueRefOrNullRef(dict, key);
        if (Unsafe.IsNullRef(ref val))
        {
            return false;
        }

        val = value;
        return true;
    }
}