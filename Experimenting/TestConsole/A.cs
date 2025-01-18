public class A
{
    public int X { get; set; }

    public List<string> Foo()
    {
        return
        [
            "Hello",
            "World"
        ];
    }

    public int Barton()
    {
        return Foo()
            .Select(x => x.Length)
            .Sum();
    }
}