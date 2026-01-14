// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var input = Console.ReadLine();
var result = input
             | int.Parse
             | Math.Abs
             | (x => x * 2);
Console.WriteLine(result);