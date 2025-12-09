using System.Text.Json;
using Extensions;
using Humanizer;

// string
var stringNumber = "12";
var stringNotNumber = "Andy";

Console.WriteLine(stringNotNumber.ValueOrDefault("this is the default value"));
Console.WriteLine(stringNumber.ValueOrDefault(25));


// enumerable
var enumerable = default(IEnumerable<int>);
if (enumerable.IsNullOrEmpty())
    Console.WriteLine("this is null");
var empty = enumerable.EmptyIfNull();
if (!empty.Any())
    Console.WriteLine("this is empty");


// dictionary
var keyValuePairs = new List<KeyValuePair<int, string>>()
{
    new(1, "one"),
    new(2, "two"),
    new(3, "third")
};

// var dict = keyValuePairs.ToDictionary();
// Console.WriteLine(dict.Humanize());

var randomString = "aram.zhamkochyan@gmail.com";
var countDict = randomString.ToCountDictionary();
Console.WriteLine(countDict.Humanize());

var players = new Dictionary<int, string>()
{
    { 1, "Beglaryan" },
    { 21, "Tiknizyan" },
    { 17, "Hovhannisyan" },
};
var lookup = players.ToLookupWithDefault("unknown");
Console.WriteLine($"Number 1 is {lookup(1)}");
Console.WriteLine($"Number 2 is {lookup(2)}");

// functional
var toFahrenheit = (decimal tempC) =>
    tempC
        .Map(x => x * 9)
        .Map(x => x / 5)
        .Tee(Console.WriteLine) // log for a reason
        .Map(x => x + 32)
        .Map(x => $"{x} degree Fahrenheit");
Console.WriteLine(toFahrenheit(36.6m));

var toFahrenheitOps = (decimal tempC) => tempC.ApplyOps(x => x * 9, x => x / 5, x => x + 32);
Console.WriteLine(toFahrenheitOps(100m));

var prependHelloToException = (string x) => $"Hello {x}";

var e1 = new Exception("my message");
var e2 = default(Exception);

Console.WriteLine(e1.DoIfNotNull(x => prependHelloToException(x.Message)));
Console.WriteLine(e2.DoIfNotNull(x => prependHelloToException(x.Message)));

var validateUserName = (string un) => un.Validate(x => !string.IsNullOrWhiteSpace(x), x => x.Length >= 2,
    x => x.Length <= 20, x => !x.Contains("Arman Bardumyan"));

Console.WriteLine($"is 'a' valid: {validateUserName("a")}");
Console.WriteLine($"is 'arman' valid: {validateUserName("arman")}");

var arrA = new[] { 'a', 'b', 'c', 'd' };
var arrB = arrA.Adjust((x, i) => i == 2, 'z');
var arrC = arrA.Adjust((x, i) => x == 'c', 'z');
Console.WriteLine(arrB.Humanize());
Console.WriteLine(arrC.Humanize());


// consecutive numbers
var arrWithCons = new[] { 1, 3, 4, 6, 8, 10 };
var arrWithoutCons = new[] { 1, 3, 6, 8, 10 };
Console.WriteLine(arrWithCons.ContainsConsecutiveNumbers());
Console.WriteLine(arrWithoutCons.ContainsConsecutiveNumbers());

// get or add in dict
var values = new Dictionary<int, string>()
{
    { 1, "one" },
    { 2, "two" },
};
var val = values.GetOrAdd(3, "three");
Console.WriteLine(JsonSerializer.Serialize(values));

var isUpdated = values.TryUpdate(2, "երկու");
Console.WriteLine(JsonSerializer.Serialize(values));