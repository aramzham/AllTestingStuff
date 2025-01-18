int[] a = [1, 4, 6, 9320, 239, 1, 23];

var b = a.Where(x => x >= 100)
    .Select(x => x * x)
    .OrderBy(x => x)
    .ToArray();

var newA = new A();
Console.WriteLine(newA.Barton());
foreach (var item in b)
{
    newA.X = 11;
    Console.WriteLine(item);
}

void Foo()
{
    Console.WriteLine("Hello");
}