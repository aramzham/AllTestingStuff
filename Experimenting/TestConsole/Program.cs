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

var c = a.Chunk(2).Select((chunk, index) => (Chunk: chunk, Number: index));
foreach (var item in c)
{
    Console.WriteLine($"Chunk {item.Number}:");
    foreach (var chunkItem in item.Chunk)
    {
        Console.WriteLine(chunkItem);
    }
}

// language=html
var html = """
           <div>
                <p>this is html</p>
           </div>
           """;