using BenchmarkDotNet.Attributes;

namespace CachingInDotNet7;

[MemoryDiagnoser(false)]
public class Benchmarks
{
    [Params(10, 1000, 10000)] public int Size { get; set; }

    [Benchmark]
    public void NormalForLoop()
    {
        for (var i = 0; i < Size; i++)
        {
            DoSomething(i);
        }
    }

    [Benchmark]
    public void ExtendedForeachLoop()
    {
        foreach (var i in 0..Size)
        {
            DoSomething(i);
        }
    }
    
    [Benchmark]
    public void MoreElegantForeachLoop()
    {
        foreach (var i in Size)
        {
            DoSomething(i);
        }
    }

    private static void DoSomething(int i)
    {
        // dummy method
    }
}