using System;
using System.Numerics;
using System.Threading;

namespace NumericalDataTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 114; //buffer size must be >= the window size
            Console.WindowWidth = 114;
            Console.WriteLine($"Max value of double: {double.MaxValue:n}"); //remove :n
            Console.WriteLine($"Epsilon: {double.Epsilon:E}"); //remove :E
            Console.WriteLine($"Positive infinity: {double.PositiveInfinity}");
            Console.WriteLine($"Negative infinity: {double.NegativeInfinity}");
            Console.WriteLine($"False string: {bool.FalseString}");
            Console.WriteLine($"True string: {bool.TrueString}");
            Console.WriteLine($"Is whitespace 5th character?: {char.IsWhiteSpace("Is whitespace 5th character?", 5)}");
            Console.WriteLine($"Is \'?\' mark is punctuation?: {char.IsPunctuation('?')}");
            var b = bool.Parse("True");
            Console.WriteLine($"b is: {b}");
            var c = char.Parse("w");
            Console.WriteLine($"c is: {c}");
            var dt = new DateTime(2017, 2, 10);
            Console.WriteLine($"Aujourd'hui on est le {dt.Date.ToShortDateString()}, c'est {dt.DayOfWeek} (en anglais)");
            Console.WriteLine($"Is this day a daylight saving time?: {dt.IsDaylightSavingTime()}");
            var ts = new TimeSpan(7, 22, 56, 20);
            Console.WriteLine($"TimeSpan substract: {ts.Subtract(new TimeSpan(3, 3, 49, 100))}");
            var biggy = BigInteger.Parse("12345678909876543211234567898765345678678675643432");
            Console.WriteLine($"Is {nameof(biggy)} power of 2?: {biggy.IsPowerOfTwo}");
            Console.WriteLine($"Is {nameof(biggy)} even(զույգ)?: {biggy.IsEven}");
            var notorious = (BigInteger)10458309481242343242;
            Console.WriteLine($"Fuckin' big integer(BigInteger.Multiply()): {notorious * biggy}");
            var str = "Foredo";
            Console.WriteLine($"Replace 'o': {str.Replace("o", "")}"); //replaces all occurences with specified symbol or string
            Console.Beep();
            Thread.Sleep(1000);
            Console.WriteLine("What's the difference between me and you?\n");
            Console.WriteLine("Hello world\r"); //i guess there is no difference on using \r with Console.WriteLine()
            Console.WriteLine("Hello beep\a"); //\a is for beep, an audio clue for the user :-)
            Console.WriteLine("We are using \"Double quotes\" in string literal");
            var stringWithEnters = @"This a very
                                                very
                                                    very
                                                        long string";
            Console.WriteLine(stringWithEnters);
            Console.WriteLine("This is a \"Verbatim\" string"); //couldn't define a verbatim string

            Console.ReadKey();
        }
    }
}
