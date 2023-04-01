using System.Linq.Expressions;
using Extensions.Attributes;

namespace Extensions;

public static class AttributeExtensions
{
    public static int GetSeasonNumber(this Type t) => t.GetCustomAttributes(true)
        .FirstOrDefault(x => x.GetType() == typeof(SeasonNumber)).Map(x => x as SeasonNumber)?.Season ?? 0;

    public static int GetAttributeInt<T>(this Type @this, Func<T, int> f) where T : class => @this
        .GetCustomAttributes(true)
        .FirstOrDefault(x => x.GetType() == typeof(T))
        .Map(x => x as T)
        .Map(f);

    public static int
        GetAttributePropInt<TObj, TAttr>(this Type @this, Expression<Func<TObj, object>> prop,
            Func<TAttr, int> selector) where TObj : class where TAttr : Attribute => @this
        .GetProperty((prop.Body as MemberExpression).Member.Name)
        .GetCustomAttributes(true)
        .FirstOrDefault(x => x.GetType() == typeof(TAttr))
        .Map(x => x as TAttr)
        .Map(x => x == null ? 0 : selector(x));
}