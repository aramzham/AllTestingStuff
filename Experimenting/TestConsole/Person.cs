namespace TestConsole;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public static bool operator true(Person? p) => p is not null;
    public static bool operator false(Person? p) => p is null;
}