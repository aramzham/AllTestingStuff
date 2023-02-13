namespace RomanMath;

public class RomanCalculator : ICalculator, IConvertToRoman, IConvertToNumeric
{
    private static Dictionary<char, int> RomanMap = new()
    {
        { 'I', 1 },
        { 'V', 5 },
        { 'X', 10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 }
    };

    public string Evaluate(string input)
    {
        return ToRoman((int)EvaluateToNumber(input));
    }

    public int ToInt(string input)
    {
        var number = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (i + 1 < input.Length && RomanMap[input[i]] < RomanMap[input[i + 1]])
                number -= RomanMap[input[i]];
            else
                number += RomanMap[input[i]];
        }

        return number;
    }

    public string ToRoman(int number)
    {
        return number switch
        {
            < 0 or > 3999 => throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999"),
            < 1 => string.Empty,
            >= 1000 => "M" + ToRoman(number - 1000),
            >= 900 => "CM" + ToRoman(number - 900),
            >= 500 => "D" + ToRoman(number - 500),
            >= 400 => "CD" + ToRoman(number - 400),
            >= 100 => "C" + ToRoman(number - 100),
            >= 90 => "XC" + ToRoman(number - 90),
            >= 50 => "L" + ToRoman(number - 50),
            >= 40 => "XL" + ToRoman(number - 40),
            >= 10 => "X" + ToRoman(number - 10),
            >= 9 => "IX" + ToRoman(number - 9),
            >= 5 => "V" + ToRoman(number - 5),
            >= 4 => "IV" + ToRoman(number - 4),
            >= 1 => "I" + ToRoman(number - 1)
        };
    }

    private double EvaluateToNumber(string input)
    {
        var expr = "(" + input + ")";
        var ops = new Stack<char>();
        var vals = new Stack<double>();

        for (var i = 0; i < expr.Length; i++)
        {
            var s = expr[i];
            switch (s)
            {
                case '(':
                case ' ':
                    break;
                case '+':
                case '-':
                case '*':
                case '/':
                    ops.Push(s);
                    break;
                case ')':
                {
                    var count = ops.Count;
                    while (count > 0)
                    {
                        var op = ops.Pop();
                        var v = vals.Pop();
                        switch (op)
                        {
                            case '+': v = vals.Pop() + v; break;
                            case '-': v = vals.Pop() - v; break;
                            case '*': v = vals.Pop() * v; break;
                            case '/': v = vals.Pop() / v; break;
                        }
                        vals.Push(v);

                        count--;
                    }

                    break;
                }
                default:
                {
                    var romanNumber = new string(expr[i..].TakeWhile(char.IsLetter).ToArray());
                    var intNumber = ToInt(romanNumber);
                    vals.Push(intNumber);
                    i += romanNumber.Length - 1;
                    break;
                }
            }
        }

        return vals.Pop();
    }
}