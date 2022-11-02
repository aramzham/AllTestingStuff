namespace CachingInDotNet7;

public ref struct CustomIntEnumerator
{
    private readonly int _end;

    public int Current { get; private set; }

    public CustomIntEnumerator(Range range)
    {
        if (range.End.IsFromEnd)
            throw new NotSupportedException("you can't go endlessly");
        
        Current = range.Start.Value - 1;
        _end = range.End.Value;
    }

    public bool MoveNext()
    {
        Current++;
        return _end >= Current;
    }
}