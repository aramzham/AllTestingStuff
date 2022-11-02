namespace CachingInDotNet7;

public static class Extensions
{
    public static CustomIntEnumerator GetEnumerator(this Range range)
    {
        return new CustomIntEnumerator(range);
    }
    
    public static CustomIntEnumerator GetEnumerator(this int number)
    {
        return new CustomIntEnumerator(new Range(0, number));
    }
}