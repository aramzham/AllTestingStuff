``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.14393.5427/1607/AnniversaryUpdate/Redstone1)
Intel Core i7-5500U CPU 2.40GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
Frequency=2338335 Hz, Resolution=427.6547 ns, Timer=TSC
.NET SDK=7.0.100-rc.2.22477.23
  [Host]     : .NET 7.0.0 (7.0.22.47203), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.47203), X64 RyuJIT AVX2


```
|                 Method |  Size |          Mean |       Error |        StdDev |        Median | Allocated |
|----------------------- |------ |--------------:|------------:|--------------:|--------------:|----------:|
|          **NormalForLoop** |    **10** |      **4.689 ns** |   **0.4572 ns** |     **1.2670 ns** |      **4.240 ns** |         **-** |
|    ExtendedForeachLoop |    10 |     13.792 ns |   0.2767 ns |     0.4141 ns |     13.784 ns |         - |
| MoreElegantForeachLoop |    10 |     42.453 ns |   0.8903 ns |     1.6722 ns |     41.739 ns |         - |
|          **NormalForLoop** |  **1000** |    **367.684 ns** |   **7.3894 ns** |     **9.3452 ns** |    **370.107 ns** |         **-** |
|    ExtendedForeachLoop |  1000 |    378.308 ns |   5.8635 ns |     9.6339 ns |    377.351 ns |         - |
| MoreElegantForeachLoop |  1000 |  2,162.191 ns |  41.3901 ns |    46.0050 ns |  2,165.629 ns |         - |
|          **NormalForLoop** | **10000** |  **3,587.058 ns** |  **71.5489 ns** |   **172.7987 ns** |  **3,537.530 ns** |         **-** |
|    ExtendedForeachLoop | 10000 |  3,524.516 ns |  45.6782 ns |    40.4926 ns |  3,543.675 ns |         - |
| MoreElegantForeachLoop | 10000 | 23,007.540 ns | 684.3028 ns | 1,861.6963 ns | 22,740.841 ns |         - |


Some interesting points to conclude:
- range loop is almost as performat as the ordinary loop for big numbers
- more elegant loop is about 8 times slower (for some reason?? :cold_sweat:)
