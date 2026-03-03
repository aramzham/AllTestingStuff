using System.Text.Json;
using TestConsole;

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

var d = a.GroupBy(k => k % 2 == 0 ? "zuyg" : "kent", r => r + 1);
foreach (var item in d)
{
    Console.WriteLine(item.Key);
    Console.WriteLine(string.Join(", ", item));
}

// language=html
var html = """
           <div>
                <p>this is html</p>
           </div>
           """;

var values = new Dictionary<int, string>
{
    { 1, "one" },
    { 2, "two" }
};

var val = values.GetOrAdd(3, "three");

Console.WriteLine(val);
Console.WriteLine(JsonSerializer.Serialize(values));

values.TryUpdate(3, "threeeee");
Console.WriteLine(JsonSerializer.Serialize(values));

var person = new Person()
{
    Name = "Armen",
    Age = 30
};

if (person)
{
    Console.WriteLine(person.Name);
}
else
{
    Console.WriteLine("Person is null");
}

#region Deadlock!!

var bgThread1 = Task.Run(() => Thread.Sleep(100));
var bgThread2 = Task.Run(() => Thread.Sleep(200));

bgThread1 = Task.Run(() => SimulateLongRunningTask(bgThread2, CancellationToken.None));
bgThread2 = Task.Run(() => SimulateLongRunningTask(bgThread1, CancellationToken.None));

await bgThread1;
await bgThread2;

async Task SimulateLongRunningTask(Task otherLongRunningFunction, CancellationToken token)
{
    await Task.Delay(TimeSpan.FromSeconds(2), token);
    await otherLongRunningFunction;
    Console.WriteLine($"Finished thread {Thread.CurrentThread.ManagedThreadId}");
}

#endregion